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
using Twitter.Core.Proxy;
using Twitter.Core.UserModels;

namespace Twitter.Core
{
    public class Account
    {
        private HttpRequest _request;
        private string fb_dtsg;

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
                    _request["Accept-Language"] = "en-US,en;q=0.9,ru-RU;q=0.8,ru;q=0.7";
                    _request["Origin"] = "https://www.facebook.com";
                    _request["Referer"] = "https://www.facebook.com/";
                    _request["Cookie"] = "sb=w77fXqttorpzs_vdoB5YkKsY; datr=IWVsYNv5HCL8qh2slf4AdSxv; wd=1858x952; c_user=100041734026467; spin=r.1003591230_b.trunk_t.1617888224_s.1_v.2_; _fbp=fb.1.1617888482148.506516284; xs=45%3APFiRsP9V-uWdcw%3A2%3A1617888222%3A13779%3A15487%3A%3AAcWzuvFJqW0OPFaregx3CcR4hVapmT22-0yEXfiCTg; fr=12EsYJVfidt6bH9Af.AWX8L0pXyw3_RdsmHqsDgfs7SkY.BgcFfv.6C.AAA.0.0.BgcFfv.AWVxjQhZ7Zw; presence=C%7B%22t3%22%3A%5B%5D%2C%22utc3%22%3A1617975288990%2C%22v%22%3A1%7D";
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
    }
}
