using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using Leaf.xNet;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Facebook.Core
{
    public class Account : IAccount
    {
        private HttpRequest _request;
        private IProxyProvider _proxyProvider;
        private string fb_dtsg;
        private string actor_id;

        public AccountData AccountData { get; set; }
        public bool IsAuthorized { get; set; }
        public bool EnableProxies { get; set; } = true;

        public Account(IProxyProvider proxyProvider)
        {
            _proxyProvider = proxyProvider;

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
                    if (cookie.name == "c_user")
                    {
                        actor_id = cookie.value;
                    }
                    storage.Add(new Cookie(cookie.name, cookie.value, cookie.path, cookie.domain));
                }
                catch { continue; }
            }
            _request.Cookies = storage;

            if (EnableProxies)
                _request.Proxy = _proxyProvider.GetProxy();

            var reconnectCount = 3;
            do
            {
                try
                {
                    _request["Accept-Language"] = "en-US,en;q=0.9,ru-RU;q=0.8,ru;q=0.7";
                    _request["Origin"] = "https://www.facebook.com";
                    _request["Referer"] = "https://www.facebook.com/";
                    _request["content-type"] = "application/x-www-form-urlencoded";

                    var response = _request.Get("https://www.facebook.com/");
                    fb_dtsg = Regex.Match(response.ToString(), "\"token\":\"(.*?)\"").Groups[1].Value;

                    if (!string.IsNullOrWhiteSpace(fb_dtsg))
                        IsAuthorized = true;
                    else
                        IsAuthorized = false;

                    break;
                }
                catch (HttpException)
                {
                    if (EnableProxies)
                    {
                        _request.Proxy = _proxyProvider.GetProxy();//Reconnecting to another proxy
                        reconnectCount--;
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    break;
                }
            } while (reconnectCount != 0);

            return IsAuthorized;
        }
        public bool Registration(string firstname, string lastname, string email, string password)
        {
            _request.AllowAutoRedirect = true;

            var response = _request.Get("http://www.facebook.com/r.php");

            var textResponse = response.ToString();

            string reg_instance = Regex.Match(textResponse, "name=\"reg_instance\"\\s+value=\"([^\"]+)").Groups[1].Value;
            string captcha_persist_data = Regex.Match(textResponse, "name=\"captcha_persist_data\"\\s+value=\"([^\"]+)").Groups[1].Value;
            string captcha_session = Regex.Match(textResponse, "name=\"captcha_session\"\\s+value=\"([^\"]+)").Groups[1].Value;
            string extra_challenge_params = Regex.Match(textResponse, "name=\"extra_challenge_params\"\\s+value=\"([^\"]+)").Groups[1].Value;
            string revision = Regex.Match(textResponse, "\"revision\":([\\d]+)").Groups[1].Value;
            string token = Regex.Match(textResponse, "\"token\":\"([^\"]+)\"").Groups[1].Value;
            string ph = Regex.Match(textResponse, "\"push_phase\":\"([^\"]+)\"").Groups[1].Value;
            string locale = Regex.Match(textResponse, "name=\"locale\"\\s+value=\"([^\"]+)").Groups[1].Value;

            _request.AddHeader("accept", "*/*");
            _request.AddHeader("accept-encoding", "gzip, deflate");
            _request.AddHeader("accept-language", "en-US,en;q=0.9,ru-RU;q=0.8,ru;q=0.7");
            _request.AddHeader("content-type", "application/x-www-form-urlencoded");
            _request.AddHeader("origin", "https://www.facebook.com");
            _request.AddHeader("referer", "https://www.facebook.com/");
            _request.AddHeader("sec-ch-ua", "\" Not A; Brand\";v=\"99\", \"Chromium\";v=\"90\", \"Google Chrome\";v=\"90\"");
            _request.AddHeader("sec-ch-ua-mobile", "?0");
            _request.AddHeader("sec-fetch-dest", "empty");
            _request.AddHeader("sec-fetch-mode", "cors");
            _request.AddHeader("sec-fetch-site", "same-origin");
            _request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36");
            _request.AddHeader("x-fb-lsd", token);

            response = _request.Post("https://www.facebook.com/ajax/bz", new RequestParams() {
                ["__a"] = "1",
                ["__dyn"] = "7wiXwNAwsUKEkxqnFw",
                ["__req"] = "1",
                ["__rev"] = revision,
                ["__user"] = "0",
                ["lsd"] = token,
                ["miny_encode_ms"] = "2",
                ["ph"] = ph,
                ["ts"] = Convert.ToInt64(DateTime.Now.Subtract(new DateTime(1970, 1, 1, 7, 0, 0)).TotalMilliseconds).ToString(),
            });

            _request.AddHeader("accept", "*/*");
            _request.AddHeader("accept-encoding", "gzip, deflate");
            _request.AddHeader("content-type", "application/x-www-form-urlencoded");
            _request.AddHeader("origin", "https://www.facebook.com");
            _request.AddHeader("referer", "https://www.facebook.com/");
            _request.AddHeader("sec-ch-ua", "\" Not A; Brand\";v=\"99\", \"Chromium\";v=\"90\", \"Google Chrome\";v=\"90\"");
            _request.AddHeader("sec-ch-ua-mobile", "?0");
            _request.AddHeader("sec-fetch-dest", "empty");
            _request.AddHeader("sec-fetch-mode", "cors");
            _request.AddHeader("sec-fetch-site", "same-origin");
            _request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36");
            _request.AddHeader("x-fb-lsd", token);

            response = _request.Post("https://www.facebook.com/ajax/register.php", new RequestParams()
            {
                ["jazoest"] = "2991",
                ["lsd"] = token,
                ["firstname"] = firstname,
                ["lastname"] = lastname,
                ["reg_email__"] = email,
                ["reg_email_confirmation__"] = email,
                ["reg_passwd__"] = password,
                ["birthday_month"] = "7",
                ["birthday_day"] = "12",
                ["birthday_year"] = "1993",
                ["birthday_age"] = "",
                ["did_use_age"] = "false",
                ["sex"] = "2",
                ["preferred_pronoun"] = "",
                ["custom_gender"] = "",
                ["referrer"] = "",
                ["asked_to_login"] = "0",
                ["use_custom_gender"] = "",
                ["terms"] = "on",
                ["ns"] = "0",
                ["ph"] = ph,//необяз
                ["ts"] = Convert.ToInt64(DateTime.Now.Subtract(new DateTime(1970, 1, 1, 7, 0, 0)).TotalMilliseconds).ToString(),//необяз
                ["ri"] = "ddc6f278-577e-4bc6-979c-95f225982372",
                ["action_dialog_shown"] = "",
                ["ignore"] = "captcha",
                ["locale"] = "en_US",
                ["reg_instance"] = reg_instance,
                ["captcha_persist_data"] = captcha_persist_data,
                ["captcha_response"] = "",
                ["__user"] = "0",
                ["__a"] = "1",
                ["__dyn"] = token,
                ["__csr"] = "",
                ["__req"] = "9",
                ["__beoa"] = "0",
                ["__pc"] = "PHASED:DEFAULT",
                ["__bhv"] = "2",
                ["dpr"] = "1",
                ["__ccg"] = "UNKNOWN",
                ["__rev"] = revision,
                ["__s"] = "nnxu6e:ml4boj:9uyqb9",
                ["__hsi"] = "6956222741722106030-0",
                ["__comet_req"] = "0",
                ["__spin_r"] = "1003699057",
                ["__spin_b"] = "trunk",
                ["__spin_t"] = "1619621818",
            });

            string error = Regex.Match(response.ToString(), "\"error\":\"([^\"]+)\"").Groups[1].Value;
            string redirect = Regex.Match(response.ToString(), "\"redirect\":\"(.*?)\"").Groups[1].Value.Replace("\\", "");

            if (!string.IsNullOrWhiteSpace(redirect))
            {
                response = _request.Get(redirect);
            }

            return true;
        }

        public bool Like(string id, string id_comment = null)
        {
            if (!IsAuthorized)
                return false;

            try
            {
                string feedbackId;
                if (id_comment is null)
                    feedbackId = Convert.ToBase64String(Encoding.Default.GetBytes($"feedback:{id}"));
                else
                    feedbackId = Convert.ToBase64String(Encoding.Default.GetBytes($"feedback:{id}_{id_comment}"));

                var response = _request.Post("https://www.facebook.com/api/graphql/", new RequestParams()
                {
                    ["fb_dtsg"] = fb_dtsg,
                    ["fb_api_caller_class"] = "RelayModern",
                    ["fb_api_req_friendly_name"] = "CometUFIFeedbackReactMutation",
                    ["variables"] = "{\"input\":{\"feedback_id\":\"" + feedbackId + "\",\"feedback_reaction\":1,\"feedback_source\":\"OBJECT\",\"feedback_referrer\":\"/settings\",\"is_tracking_encrypted\":true,\"tracking\":[\"AZXYrwgk4t_cIv8aoiAIwAY2R7y8kmYqQln2RnWB7NDId_N5yqDRmBTz2prkyzEyVU5H66oo_xWE7zlpnFEU_DEKRRKQSXCuVObBS6gl7rryWCzriGC-33KMg06vcz-u5EcvpFh5cxNg1CstegWLW8WKSW2PnKSWd9-k-Axj438xCMv3FvvjPXhuwBLscWXFaHvcNxaOdcjz_37MCCD7PGaAKBKmTqk-GM6oCIO18-f7qhIhzyC9HpavpwouKZMfNhJqRNQz54eYi5kti0BBuZ7JdneE9Rov6IuEjx3XP5a_sSQK3-e2j1s8erayb1AP8t5TMgDwPWGc3qxd_dKd5Ch2oENBjFvG9bsoW6ze9ogOnDzO3zPJyX-RVzC5ohjiHZOWQrfchq3tjsVUDiJXBlSykJhxUKEJUAdDFQdErBfdczvflw5Ap7zwssJl49wBL8Do2PKG_zbiUxKVxqMeNRnR\"],\"session_id\":\"d3462960-8088-4466-b01b-0086319e9653\",\"actor_id\":\"" + actor_id + "\",\"client_mutation_id\":\"9\"},\"useDefaultActor\":false}",
                    ["doc_id"] = "3852462771486439"
                });

                return response.IsOK && response.ToString().Contains("can_viewer_react");
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = _proxyProvider.GetProxy();
                return false;
            }
        }
        public bool Comment(string id, string message)
        {
            if (!IsAuthorized)
                return false;

            try
            {
                string feedbackId = Convert.ToBase64String(Encoding.Default.GetBytes($"feedback:{id}"));
                string idempotence_token = Guid.NewGuid().ToString();

                var response = _request.Post("https://www.facebook.com/api/graphql/", new RequestParams()
                {
                    ["fb_dtsg"] = fb_dtsg,
                    ["fb_api_caller_class"] = "RelayModern",
                    ["fb_api_req_friendly_name"] = "CometUFICreateCommentMutation",
                    ["variables"] = "{\"displayCommentsFeedbackContext\":null,\"displayCommentsContextEnableComment\":null,\"displayCommentsContextIsAdPreview\":null,\"displayCommentsContextIsAggregatedShare\":null,\"displayCommentsContextIsStorySet\":null,\"feedLocation\":\"PERMALINK\",\"feedbackSource\":2,\"focusCommentID\":null,\"includeNestedComments\":false,\"input\":{\"attachments\":null,\"feedback_id\":\"" + feedbackId + "\",\"formatting_style\":null,\"message\":{\"ranges\":[],\"text\":\"" + message + "\"},\"reply_target_clicked\":false,\"is_tracking_encrypted\":true,\"tracking\":[\"AZVVpf17x0LB3niYRosnueqKsHA3bLulUcPxBIpaR7owz1XFHsgwG7oBQISS0L03X7zhAh07OF5nMtMDt_FK_1ipULhUvfkGlk4jyyVVLHlQR065a6lrgdmeKg0YP5vzpOsAahylToqXKG3zuXSgWTfqGUku_pk60C5MD7D57SJ19wHJiKRQtkkX-pmcjhsWiS4hgE4lKNJPB4xRS0ZWfMG-lhDw7Qwtr-MwhReHK2nlcp7Yu9BMrmol_zJghGk_z_bwl3kJf-daFKeDEXZROPncKNZ7mK6HhNaDNZbj-cTjA-44lNbFAx7WJxSfBQq-bKLX4MmA4myAyxlXZN7zGprIkO57oePkK0_eZflpsEeunBrHnWqPRnUY7ihsAcSqhlgHrStTsNlo_7X4v3D44atY8Wzoj5z3UhMFzLYtQmN3FGvO9yAAsN6ay_zSozwyoeQxCYs3AHyaS-N1aYF0cgQ5P9j3ukh5SovRMW398watCg7P4i3zMMeN8bY_l-tphK0\"],\"feedback_source\":\"OBJECT\",\"idempotence_token\":\"client:" + idempotence_token + "\",\"session_id\":\"1b84acc8-0b94-4c42-8f44-e5748a7c10cd\",\"actor_id\":\"" + actor_id + "\",\"client_mutation_id\":\"5\"},\"scale\":1,\"useDefaultActor\":false,\"UFI2CommentsProvider_commentsKey\":\"CometSinglePostRoute\"}",
                    ["server_timestamps"] = "true",
                    ["doc_id"] = "5357613787646767"
                });

                return response.IsOK && !response.ToString().Contains("errors");
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = _proxyProvider.GetProxy();
                return false;
            }
        }
        public bool CommentStream(string id, string message)
        {
            if (!IsAuthorized)
                return false;

            try
            {
                string feedbackId = Convert.ToBase64String(Encoding.Default.GetBytes($"feedback:{id}"));
                string idempotence_token = Guid.NewGuid().ToString();

                /*var startMutation = _request.Post("https://www.facebook.com/api/graphql/", new RequestParams()
                {
                    ["fb_dtsg"] = fb_dtsg,
                    ["fb_api_caller_class"] = "RelayModern",
                    ["fb_api_req_friendly_name"] = "CometUFILiveTypingBroadcastMutation_StartMutation",
                    ["variables"] = "{\"input\":{\"feedback_id\":\"" + feedbackId + "\",\"session_id\":\"3a43c801-a271-4fba-a693-fbe709d963ff\",\"actor_id\":\"100041734026467\",\"client_mutation_id\":\"3\"}}",
                    ["doc_id"] = "4132343240150432"
                });
                var stopMutation = _request.Post("https://www.facebook.com/api/graphql/", new RequestParams()
                {
                    ["fb_dtsg"] = fb_dtsg,
                    ["fb_api_caller_class"] = "RelayModern",
                    ["fb_api_req_friendly_name"] = "CometUFILiveTypingBroadcastMutation_StopMutation",
                    ["variables"] = "{\"input\":{\"feedback_id\":\"" + feedbackId + "\",\"session_id\":\"3a43c801-a271-4fba-a693-fbe709d963ff\",\"actor_id\":\"100041734026467\",\"client_mutation_id\":\"4\"}}",
                    ["doc_id"] = "2449415248515863"
                });
                */
                var response = _request.Post("https://www.facebook.com/api/graphql/", new RequestParams()
                {
                    ["fb_dtsg"] = fb_dtsg,
                    ["fb_api_caller_class"] = "RelayModern",
                    ["fb_api_req_friendly_name"] = "GFICreateCommentMutation",
                    ["variables"] = "{\"feedLocation\":\"TAHOE\",\"input\":{\"attachments\":null,\"feedback_id\":\"" + feedbackId + "\",\"formatting_style\":null,\"message\":{\"ranges\":[],\"text\":\"" + message + "\"},\"reply_target_clicked\":false,\"is_tracking_encrypted\":true,\"tracking\":[],\"live_video_timestamp\":5903,\"feedback_source\":\"TAHOE\",\"idempotence_token\":\"client:" + idempotence_token + "\",\"session_id\":\"7532aed4-06d1-4202-b39a-fdb6f3452761\",\"actor_id\":\"" + actor_id + "" +
                    "\",\"client_mutation_id\":\"7\"},\"scale\":1,\"useDefaultActor\":false}",
                    ["server_timestamps"] = "true",
                    ["doc_id"] = "4014396065250476"
                });

                return response.IsOK && !response.ToString().Contains("errors");
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = _proxyProvider.GetProxy();
                return false;
            }
        }
    }
}
