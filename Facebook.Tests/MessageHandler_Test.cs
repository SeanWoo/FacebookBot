using System;
using System.Collections.Generic;
using Facebook.Core;
using Facebook.Core.Interfaces;
using Xunit;
using Moq;
using Facebook.CLI;
using System.IO;

namespace Facebook.Tests
{
    public class MessageHandler_Test
    {
        private IAccount _account;
        private MessageHandler _messageHandler = new MessageHandler();

        public MessageHandler_Test()
        {
            _account = Mock.Of<IAccount>(x => x.Authorization() == true && 
                                        x.Comment("123", "hi") == true &&
                                        x.Comment("123", "hello") == true &&
                                        x.Like("123", null) == true &&
                                        x.Like("123", "321") == true);
            _account.Authorization();
        }
        [Fact]
        public void StartLiker_ThreeLikes_ThreeLikes()
        {
            var accounts = new List<IAccount>();

            accounts.Add(_account);
            accounts.Add(_account);
            accounts.Add(_account);

            var result = _messageHandler.StartLiker(accounts, "-c", "3", "123");

            Assert.Equal(3, result);
        }
        [Fact]
        public void StartLiker_ThreeLikes_TwoLikes()
        {
            var accounts = new List<IAccount>();

            accounts.Add(_account);
            accounts.Add(_account);
            accounts.Add(_account);

            var result = _messageHandler.StartLiker(accounts, "-c", "2", "123");

            Assert.Equal(2, result);
        }
        [Fact]
        public void StartLiker_ThreeLikesComment_TwoLikes()
        {
            var accounts = new List<IAccount>();

            accounts.Add(_account);
            accounts.Add(_account);
            accounts.Add(_account);

            var result = _messageHandler.StartLiker(accounts, "-c", "2", "123/321");

            Assert.Equal(2, result);
        }
        [Fact]
        public void StartComments_ThreeComment_TwoComments()
        {
            var accounts = new List<IAccount>();

            accounts.Add(_account);
            accounts.Add(_account);
            accounts.Add(_account);

            var tempFile = Path.GetTempFileName();

            File.WriteAllLines(tempFile, new string[] { 
                "hi",
                "hello"
            });

            var result = _messageHandler.StartComments(accounts, "-c", "2", "-f", tempFile, "123");

            File.Delete(tempFile);

            Assert.Equal(2, result);
        }
    }
}
