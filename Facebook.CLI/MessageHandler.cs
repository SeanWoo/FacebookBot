using Facebook.Core.Interfaces;
using Facebook.CLI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Facebook.Core.Proxy;
using Facebook.Core;

namespace Facebook.CLI
{
    public class MessageHandler
    {
        private const string PATH_TO_PROXY = "proxies.txt";
        private const string PATH_TO_SETTINGS = "settings.txt";
        private const string PATH_TO_ACCOUNT = "accounts.json";

        private IEnumerable<IAccount> accounts;

        public event Action<string[]> OnMessage;
        public event Func<IEnumerable<IAccount>, string[], int> OnStartMessage;

        public MessageHandler()
        {
            if (!File.Exists(PATH_TO_PROXY))
                File.Create(PATH_TO_PROXY).Close();

            if (!File.Exists(PATH_TO_SETTINGS))
                File.Create(PATH_TO_SETTINGS).Close();

            if (!File.Exists(PATH_TO_ACCOUNT))
                File.Create(PATH_TO_ACCOUNT).Close();

            SettingsReader.LoadSettings(PATH_TO_SETTINGS);
            ProxyProvider.LoadProxy(PATH_TO_PROXY, SettingsReader.ProxyType);
            accounts = DataLoader.GetAccountsList(PATH_TO_ACCOUNT).Select(x => new Account(x));
        }

        public bool TryExecuteCommand(string nameCommand, string input, Action<string[]> action)
        {
            var args = input.Split();
            if (args.Length > 0 && args[0] == nameCommand)
            {
                args = args.Skip(1).ToArray();
                action(args);
                return true;
            }
            return false;
        }
        public bool TryExecuteCommand(string nameCommand, string input, Func<IEnumerable<IAccount>, string[], int> action)
        {
            var args = input.Split();
            if (args.Length > 0 && args[0] == nameCommand)
            {
                args = args.Skip(1).ToArray();
                action(accounts, args);
                return true;
            }
            return false;
        }

        public void Start()
        {
            string command;
            while ((command = Console.ReadLine()) != "exit")
            {
                var result = false;

                result |= TryExecuteCommand("setProxy", command);
                result |= TryExecuteCommand("setAccount", command);

                result |= TryExecuteCommand("like", command);
                result |= TryExecuteCommand("comment", command);

                if (!result)
                    Console.WriteLine("Такой комманды нету");
            }
        }

        public int StartLiker(IEnumerable<IAccount> accounts, params string[] args)
        {
            var ids = args.Last().Split('/');

            var postId = ids.First();
            var commentId = ids.Length > 1 ? ids.Last() : null;

            int.TryParse(args.SkipWhile(x => x != "-c").Take(2).Last(), out int countLikes);

            if(countLikes == 0)
            {
                Console.WriteLine("Ошибка синтаксиса");
                return 0;
            }

            if(countLikes > accounts.Count())
            {
                Console.WriteLine("Вы указали слишком большое кол-во лайков");
                return 0;
            }

            var counterLikes = 0;
            foreach (var account in accounts)
            {
                if (countLikes == 0) break;
                if (account.Authorization())
                {
                    if (account.Like(postId, commentId))
                    {
                        counterLikes++;
                        countLikes--;
                    }
                }
            }

            return counterLikes;
        }
        public int StartComments(IEnumerable<IAccount> accounts, params string[] args)
        {
            var postId = args.Last();

            int.TryParse(args.SkipWhile(x => x != "-c").Take(2).Last(), out int countComments);

            var filePath = args.SkipWhile(x => x != "-f").Take(2).Last();

            if (countComments == 0)
            {
                Console.WriteLine("Ошибка синтаксиса");
                return 0;
            }

            if (countComments > accounts.Count())
            {
                Console.WriteLine("Вы указали слишком большое кол-во лайков");
                return 0;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Такого файла не существует");
                return 0;
            }

            var comments = File.ReadAllLines(filePath);
            var counterComments = 0;
            foreach (var account in accounts)
            {
                if (countComments == 0) break;
                if (account.Authorization())
                {
                    if (account.Comment(postId, comments.GetRandom()))
                    {
                        counterComments++;
                        countComments--;
                    }
                }
            }

            return counterComments;
        }
    }
}
