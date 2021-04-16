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
        [Fact]
        public void Like()
        {
            var result = _account.Like("10166091207755725");
            Assert.True(result);
        }
        [Fact]
        public void LikeComment()
        {
            var result = _account.Like("10166091207755725", "100919705469766");
            Assert.True(result);
        }
        [Fact]
        public void Comment()
        {
            var result = _account.Comment("3392982234152953", "Ohh, nice!");
            Assert.True(result);
        }
    }
}
