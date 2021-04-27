using System.Collections.Generic;

namespace Facebook.Shared.Models
{
    public class AccountData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<CookieModel> Cookies { get; set; }
    }
}