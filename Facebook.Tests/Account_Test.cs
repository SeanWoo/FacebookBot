using System;
using System.Collections.Generic;
using Twitter.Core;
using Xunit;

namespace Facebook.Tests
{
    public class Account_Test
    {
        private Account _account;
        public Account_Test()
        {
            _account = new Account(new AccountData() { 
                Cookies = new List<CookieModel>()
                {
                    new CookieModel()
                }
            });
            _account.EnableProxies = false;
            _account.Authorization();
        }
        //
        [Fact]
        public void Like()
        {
            var result = _account.Like("10158057603891104");
            Assert.True(result);
        }
        [Fact]
        public void LikeComment()
        {
            var result = _account.Like("10158057603891104", "738369583540583");
            Assert.True(result);
        }
    }
}
