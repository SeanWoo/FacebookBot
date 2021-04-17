using Facebook.Core.Interfaces;
using Leaf.xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Facebook.Core.Proxy;

namespace Facebook.Core
{
    public class Account : IAccount
    {
        private HttpRequest _request;
        private string fb_dtsg;

        public AccountData AccountData { get; private set; }
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
                    storage.Add(new Cookie(cookie.name, cookie.value, cookie.path, cookie.domain));
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
                    _request["Accept-Language"] = "en-US,en;q=0.9,ru-RU;q=0.8,ru;q=0.7";
                    _request["Origin"] = "https://www.facebook.com";
                    _request["Referer"] = "https://www.facebook.com/";
                    _request["Cookie"] = "sb=w77fXqttorpzs_vdoB5YkKsY; datr=IWVsYNv5HCL8qh2slf4AdSxv; c_user=100041734026467; _fbp=fb.1.1617888482148.506516284; wd=1858x1009; xs=45%3APFiRsP9V-uWdcw%3A2%3A1617888222%3A13779%3A15487%3A%3AAcUCgcC8rxlNdJakd-4h0fnFil0uaBTXqzUoi2_Db1c; spin=r.1003606059_b.trunk_t.1618236726_s.1_v.2_; fr=1SufvbJEu857Sl8Gy.AWXvKPVme7rmXMHGMHsbKLGWZ-M.BgdYXm.6C.AAA.0.0.BgdYXm.AWUJcPODiXE";
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
                        _request.Proxy = ProxyProvider.GetProxy();//Reconnecting to another proxy
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
                    ["variables"] = "{\"input\":{\"feedback_id\":\"" + feedbackId + "\",\"feedback_reaction\":1,\"feedback_source\":\"OBJECT\",\"feedback_referrer\":\"/settings\",\"is_tracking_encrypted\":true,\"tracking\":[\"AZXYrwgk4t_cIv8aoiAIwAY2R7y8kmYqQln2RnWB7NDId_N5yqDRmBTz2prkyzEyVU5H66oo_xWE7zlpnFEU_DEKRRKQSXCuVObBS6gl7rryWCzriGC-33KMg06vcz-u5EcvpFh5cxNg1CstegWLW8WKSW2PnKSWd9-k-Axj438xCMv3FvvjPXhuwBLscWXFaHvcNxaOdcjz_37MCCD7PGaAKBKmTqk-GM6oCIO18-f7qhIhzyC9HpavpwouKZMfNhJqRNQz54eYi5kti0BBuZ7JdneE9Rov6IuEjx3XP5a_sSQK3-e2j1s8erayb1AP8t5TMgDwPWGc3qxd_dKd5Ch2oENBjFvG9bsoW6ze9ogOnDzO3zPJyX-RVzC5ohjiHZOWQrfchq3tjsVUDiJXBlSykJhxUKEJUAdDFQdErBfdczvflw5Ap7zwssJl49wBL8Do2PKG_zbiUxKVxqMeNRnR\"],\"session_id\":\"d3462960-8088-4466-b01b-0086319e9653\",\"actor_id\":\"100041734026467\",\"client_mutation_id\":\"9\"},\"useDefaultActor\":false}",
                    ["doc_id"] = "3852462771486439"
                });

                return response.IsOK && response.ToString().Contains("can_viewer_react");
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
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
                    ["fb_api_req_friendly_name"] = "CometUFICreateCommentMutation",
                    ["variables"] = "{\"displayCommentsFeedbackContext\":null,\"displayCommentsContextEnableComment\":null,\"displayCommentsContextIsAdPreview\":null,\"displayCommentsContextIsAggregatedShare\":null,\"displayCommentsContextIsStorySet\":null,\"feedLocation\":\"PERMALINK\",\"feedbackSource\":2,\"focusCommentID\":null,\"includeNestedComments\":false,\"input\":{\"attachments\":null,\"feedback_id\":\"" + feedbackId + "\",\"formatting_style\":null,\"message\":{\"ranges\":[],\"text\":\"" + message + "\"},\"reply_target_clicked\":false,\"is_tracking_encrypted\":true,\"tracking\":[\"AZVVGcwZhxlRxjJc3jYRLOTcqRJCu7ICWaHF0xgjnJCvh315rlYWlZ8MpGIoACbppgZPs8Nb1AvI2BhSdbmtIgk_B1G9U-scDqVW2jphQfQgKjWHgj0LfQQ7uPQv6q10p9kfh7jCijmsH32RN5AfQYsfAE3ehn5P0iCXxBqBsPlGE9Z5rAXgoErKDDdN9HfPtNfUevinsaF8dsGNN4D6bbgN9BuBscQyUbc_ZPgndmC3m-qimrEi3zp2o2MfiUplYAXaQW_FHu0UcYMdSwlBqPku_Plia20mQOtSnZQXIkH2YzQwighwvDYhesY4X9pJjKOY2GwQwLm3qlm9OkKlfoMSj03BlahTh_PXWxA1sMlS8cG68MpV3zzMWxGD4tTt6lA3zNv6fOr6gChAjg0J4ciJEIw_U__ergGnezfyzoXlEiDCVcUQKaPnRwD5RsTqjET7nHVOXeTT8VBMK1aIO4QX\"],\"feedback_source\":\"OBJECT\",\"idempotence_token\":\"client:378ff319-6538-4857-aa20-2adfac851a62\",\"session_id\":\"3a43c801-a271-4fba-a693-fbe709d963ff\",\"actor_id\":\"100041734026467\",\"client_mutation_id\":\"5\"},\"scale\":1,\"useDefaultActor\":false,\"UFI2CommentsProvider_commentsKey\":\"CometSinglePostRoute\"}",
                    ["server_timestamps"] = "true",
                    ["doc_id"] = "3464759646958593"
                });

                return response.IsOK && !response.ToString().Contains("errors");
            }
            catch (HttpException)
            {
                if (EnableProxies)
                    _request.Proxy = ProxyProvider.GetProxy();
                return false;
            }
        }
    }
}
