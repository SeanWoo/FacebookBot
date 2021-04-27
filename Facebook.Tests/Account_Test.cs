using System;
using System.Collections.Generic;
using Facebook.Core;
using Xunit;
using Moq;
using Facebook.Shared.Models;

namespace Facebook.Tests
{
    public class Account_Test
    {
        private Account _account;
        public Account_Test()
        {
            _account = new Account(new ProxyProvider(new DataLoader()));
            _account.AccountData = new DataLoader().GetAccountsList(SharedData.PATH_TO_ACCOUNT)[0];
            _account.EnableProxies = false;
            _account.Authorization();
        }
        [Fact]
        public void Like_PostId_True()
        {
            var result = _account.Like("10166091207755725");
            Assert.True(result);
        }
        [Fact]
        public void Like_PostIdAndCommentId_True()
        {
            var result = _account.Like("10166091207755725", "100919705469766");
            Assert.True(result);
        }
        [Fact]
        public void Comment_PostId_True()
        {
            var result = _account.Comment("599865248081271", "Ohh, nice!");
            Assert.True(result);
        }
        [Fact]
        public void CommentStream_PostId_True()
        {
            var result = _account.CommentStream("330789768663346", "Ohh, nice!");
            Assert.True(result);
        }
    }
}
