using Facebook.Bll.Helpers;
using Facebook.Shared.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace Facebook.Bll
{
    public class BotRunner
    {
        private const string PATH_TO_PROXY = "proxies.txt";
        private const string PATH_TO_SETTINGS = "settings.txt";
        private const string PATH_TO_ACCOUNT = "accounts.json";

        private MessageListener _listener;
        private DataManager _dataManager;
        private IProxyProvider _proxyProvider;

        public BotRunner(MessageListener listener, DataManager dataManager)
        {
            _listener = listener;
            _dataManager = dataManager;
        }
        public void Start()
        {
            _listener.OnMessage += Listener_OnMessage;
        }
        public void Stop()
        {
            _listener.OnMessage -= Listener_OnMessage;
        }

        private void Listener_OnMessage(string nameCommand, string[] args)
        {
            switch (nameCommand)
            {
                case "like":
                    StartLiker(args);
                    break;
                case "comment":
                    StartComment(args);
                    break;
                default:
                    break;
            }
        }

        private int StartLiker(string[] args)
        {
            var ids = args.Last().Split('/');

            var postId = ids.First();
            var commentId = ids.Length > 1 ? ids.Last() : null;

            int.TryParse(args.SkipWhile(x => x != "-c").Take(2).Last(), out int countLikes);

            if (countLikes == 0)
            {
                Console.WriteLine("Ошибка синтаксиса");
                return 0;
            }

            if (countLikes > _dataManager.Accounts.Count())
            {
                Console.WriteLine("Вы указали слишком большое кол-во лайков");
                return 0;
            }

            var counterLikes = 0;
            foreach (var account in _dataManager.Accounts)
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

        private int StartComment(string[] args)
        {
            var postId = args.Last();

            int.TryParse(args.SkipWhile(x => x != "-c").Take(2).Last(), out int countComments);

            var filePath = args.SkipWhile(x => x != "-f").Take(2).Last();

            if (countComments == 0)
            {
                Console.WriteLine("Ошибка синтаксиса");
                return 0;
            }

            if (countComments > _dataManager.Accounts.Count())
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
            foreach (var account in _dataManager.Accounts)
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
