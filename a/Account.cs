using Leaf.xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Twitter.Core.Proxy;
using Twitter.Core.UserModels;

namespace Twitter.Core
{
    public class Account
    {
        private HttpRequest _request;

        public AccountData AccountData { get; private set; }
        public VerifyCredentials VerifyCredentials { get; private set; }
        public bool IsAuthorized { get; private set; }
        public bool EnableProxies { get; set; } = true;

        public Account(AccountData accountData)
        {
            AccountData = accountData;

            _request = new HttpRequest()
            {
                IgnoreInvalidCookie = true,
                IgnoreProtocolErrors = true,
                AllowAutoRedirect = false,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0"
            };
        }

        public bool Authorization()
        {
            if (!AccountData.Cookies.Any())
                return false;

            var storage = new CookieStorage();
            foreach (var cookie in AccountData.Cookies)
            {
                try
                {
                    storage.Add(new Cookie(cookie.name, cookie.value, cookie.path, cookie.host_key));
                }
                catch { continue; }
            }
            _request.Cookies = storage;

            if (EnableProxies)
                _request.Proxy = ProxyProvider.GetProxy();

            var reconnectCount = 3;
            do
            {
                try
                {
                    _request.AddHeader("Pragma", "no-cache");
                    _request.AddHeader("Cache-Control", "no-cache");
                    _request.AddHeader("Origin", "https://twitter.com");
                    _request.AddHeader("Upgrade-Insecure-Requests", "1");
                    _request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                    _request.AddHeader("Referer", "https://google.com");
                    _request.AddHeader(HttpHeader.AcceptLanguage, "ru,en;q=0.9");

                    _request.Get("https://twitter.com").None(); //Get cookies and check proxy
                    break;
                }
                catch (HttpException)
                {
                    if (EnableProxies)
                        _request.Proxy = ProxyProvider.GetProxy();
                    reconnectCount--;
                }
                catch
                {
                    reconnectCount--;
                }
            } while (reconnectCount != 0);

            APIBuildHeaders();
            try
            {
                var response = _request.Get("https://api.twitter.com/1.1/account/verify_credentials.json");
                if (response.IsOK)
                {
                    VerifyCredentials = JsonConvert.DeserializeObject<VerifyCredentials>(response.ToString());
                    AccountData.Username = VerifyCredentials.ScreenName;

                    IsAuthorized = true;
                }
                else
                {
                    IsAuthorized = false;
                }
                return IsAuthorized;
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
                return IsAuthorized;
            }
            catch (Exception)
            {
                return IsAuthorized;
            }
        }
        public bool Like(string id)
        {
            if (!IsAuthorized)
                return false;

            APIBuildHeaders();
            try
            {
                var response = _request.Post("https://api.twitter.com/1.1/favorites/create.json", new RequestParams()
                {
                    ["tweet_mode"] = "extended",
                    ["id"] = id
                });

                return response.IsOK;
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
                return false;
            }
        }
        public bool Unlike(string id)
        {
            if (!IsAuthorized)
                return false;

            APIBuildHeaders();
            try
            {
                var response = _request.Post("https://api.twitter.com/1.1/favorites/destroy.json", new RequestParams()
                {
                    ["tweet_mode"] = "extended",
                    ["id"] = id
                });
                return response.IsOK;
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
                return false;
            }
        }
        public bool Retweet(string id)
        {
            if (!IsAuthorized)
                return false;

            APIBuildHeaders();
            try
            {
                var response = _request.Post("https://api.twitter.com/1.1/statuses/retweet/" + id + ".json");
                return response.IsOK;
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
                return false;
            }
        }
        public bool Unretweet(string id)
        {
            if (!IsAuthorized)
                return false;

            APIBuildHeaders();
            try
            {
                var response = _request.Post("https://api.twitter.com/1.1/statuses/unretweet/" + id + ".json");
                return response.IsOK;
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
                return false;
            }
        }
        public bool Subscribe(string id)
        {
            if (!IsAuthorized)
                return false;

            APIBuildHeaders();
            try
            {
                var response = _request.Post("https://api.twitter.com/1.1/friendships/create.json", new RequestParams()
                {
                    ["include_profile_interstitial_type"] = "1",
                    ["include_blocking"] = "1",
                    ["include_blocked_by"] = "1",
                    ["include_followed_by"] = "1",
                    ["include_want_retweets"] = "1",
                    ["include_mute_edge"] = "1",
                    ["include_can_dm"] = "1",
                    ["include_can_media_tag"] = "1",
                    ["skip_status"] = "1",
                    ["id"] = id
                });
                return response.IsOK;
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
                return false;
            }
        }
        public string CreateTweet(string text, string pathToImage = null)
        {
            return CreateComment(null, text, pathToImage);
        }
        public string CreateComment(string id, string username, string text, string pathToImage = null)
        {
            if (!IsAuthorized)
                return null;

            APIBuildHeaders();
            string imageIds = null;
            if (!string.IsNullOrWhiteSpace(pathToImage) && File.Exists(pathToImage))
            {
                imageIds = LoadImage(pathToImage);
                if (imageIds is null)
                    return null;
            }

            try
            {
                var response = _request.Post("https://api.twitter.com/1.1/statuses/update.json", new RequestParams()
                {
                    ["status"] = $"@{username} {text}",
                    ["media_ids"] = imageIds != null ? imageIds : "",
                    ["in_reply_to_status_id"] = id != null ? id : ""
                });
                if (response.IsOK)
                {
                    return (string)JsonConvert.DeserializeObject<Dictionary<string, object>>(response.ToString())["id_str"];
                }
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
                return null;
            }
            return null;
        }
        public bool Delete(string id)
        {
            if (!IsAuthorized)
                return false;

            APIBuildHeaders();
            try
            {
                var response = _request.Post("https://api.twitter.com/1.1/statuses/destroy/" + id + ".json");
                return response.IsOK;
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
                return false;
            }
        }
        public string Move(string address)
        {
            if (!IsAuthorized)
                return null;

            try
            {
                return _request.Get(address).ToString();
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
                return null;
            }
        }
        public List<TweetData> GetTweets(string screen_name, int count, bool exclude_replies = true)
        {
            if (!IsAuthorized)
                return null;

            APIBuildHeaders();
            string json = "";
            try
            {
                //json = _request.Get("https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=" + screen_name + "&count=" + count + "&exclude_replies=" + exclude_replies.ToString().ToLower()).ToString();
                //var tweetData = JsonConvert.DeserializeObject<List<TweetData>>(json);

                var userInfo = GetUserInfo(screen_name);
                if (userInfo is null)
                    return null;
                
                json = _request.Get("https://twitter.com/i/api/2/timeline/profile/" + userInfo.IdStr + ".json?", new RequestParams()
                {
                    ["include_profile_interstitial_type"] = "1",
                    ["include_blocking"] = "1",
                    ["include_blocked_by"] = "1",
                    ["include_followed_by"] = "1",
                    ["include_want_retweets"] = "1",
                    ["include_mute_edge"] = "1",
                    ["include_can_dm"] = "1",
                    ["include_can_media_tag"] = "1",
                    ["skip_status"] = "1",
                    ["cards_platform"] = "Web-12",
                    ["include_cards"] = "1",
                    ["include_ext_alt_text"] = "true",
                    ["include_quote_count"] = "true",
                    ["include_reply_count"] = "1",
                    ["tweet_mode"] = "extended",
                    ["include_entities"] = "true",
                    ["include_user_entities"] = "true",
                    ["include_ext_media_color"] = "true",
                    ["include_ext_media_availability"] = "true",
                    ["send_error_codes"] = "true",
                    ["simple_quoted_tweet"] = "true",
                    ["include_tweet_replies"] = "false",
                    ["count"] = count,
                    ["userId"] = userInfo.IdStr,
                    ["ext"] = "mediaStats,highlightedLabel",
                }).ToString();
                
                JObject tweetData = JObject.Parse(json);
                var tweets = tweetData["globalObjects"]["tweets"];
                File.AppendAllText("tweets.txt", tweets.ToString());
                var resList = tweets.Select(x => new TweetData()
                    {
                        IdStr = (string)x.First["id_str"],
                        CreatedAt = DateTime.ParseExact(x.First["created_at"].ToString(), "ddd MMM dd HH:mm:ss zzzz yyyy", CultureInfo.InvariantCulture),
                        Content = (string)x.First["full_text"],
                        IsRetweet = x.First["retweeted_status_id_str"] != null,
                        User = new User()
                        {
                            ScreenName = userInfo.ScreenName
                        }
                    }).OrderByDescending(x => x.CreatedAt).ToList();

                return resList;
            }
            catch (HttpException ex)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
                File.AppendAllText("gettweets.txt", ex.HttpStatusCode + " | " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                File.AppendAllText("gettweets.txt", ex.Message);
                return null;
            }
        }
        public UserInfo GetUserInfo(string screen_name)
        {
            if (!IsAuthorized)
                return null;

            APIBuildHeaders();
            var result = _request.Get("https://api.twitter.com/1.1/users/show.json?screen_name=" + screen_name);
            
            if (result.IsOK)
                return JsonConvert.DeserializeObject<UserInfo>(result.ToString());
            return null;
        }

        private void APIBuildHeaders()
        {
            _request["Accept-Language"] = "ru,en;q=0.9";
            _request["Authorization"] = "Bearer AAAAAAAAAAAAAAAAAAAAANRILgAAAAAAnNwIzUejRCOuH5E6I8xnZz4puTs%3D1Zv7ttfk8LF81IUq16cHjhLTvJu4FA33AGWWjCpTnA";
            _request["Origin"] = "https://twitter.com";
            _request["Referer"] = "https://twitter.com/home";
            foreach (Cookie cookie in _request.Cookies.GetCookies("https://twitter.com/"))
            {
                if (cookie.Name != "ct0")
                    continue;
                _request["x-csrf-token"] = cookie.Value;
                break;
            }
            _request["x-twitter-active-user"] = "yes";
            _request["x-twitter-auth-type"] = "OAuth2Session";
            _request["x-twitter-client-language"] = "en";
        }
        private string LoadImage(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            string total_bytes = fileInfo.Length.ToString();
            string media_type = fileInfo.Extension.Remove(0, 1);

            APIBuildHeaders();
            try
            {
                var response = _request.Post("https://upload.twitter.com/i/media/upload.json", new RequestParams()
                {
                    ["command"] = "INIT",
                    ["total_bytes"] = total_bytes,
                    ["media_type"] = "image/" + media_type,
                    ["media_category"] = "tweet_image",
                });
                if (!response.IsOK)
                    return null;

                var ids = Regex.Match(response.ToString(), "(.*)\"media_id_string\":\"(.*)\",\"expires_after_secs(.*)").Groups[2].ToString();


                _request.Post("https://upload.twitter.com/i/media/upload.json?command=APPEND&media_id=" + ids + "&segment_index=0", new MultipartContent()
                {
                    {new FileContent(path), "media", "QWeqwewqeqw"}
                });
                if (!response.IsOK)
                    return null;

                _request.Post("https://upload.twitter.com/i/media/upload.json", new RequestParams()
                {
                    ["command"] = "FINALIZE",
                    ["media_id"] = ids
                });

                if (!response.IsOK)
                    return null;

                return ids;
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
                return null;
            }
        }
    }
}
