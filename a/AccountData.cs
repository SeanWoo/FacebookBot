using System.Collections.Generic;

namespace Twitter.Core
{
    public class AccountData
    {
        public string Username { get; set; }
        public List<CookieModel> Cookies { get; set; }
    }
}