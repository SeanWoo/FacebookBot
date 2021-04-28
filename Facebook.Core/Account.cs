using Leaf.xNet;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Facebook.Shared.Interfaces;
using DryIoc;
using Facebook.Shared.Models;

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
