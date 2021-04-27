using Facebook.CLI;
using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using Facebook.Shared.Models.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bll.Handlers
{
    public class CommentHandler : IMessageHandler
    {
        private IEnumerable<IAccount> _accounts;

        public CommentHandler(IFactory<IAccount> accounts, IProxyProvider provider)
        {
            _accounts = accounts.Get();
            provider.LoadProxy(SharedData.PATH_TO_PROXY, SettingsReader.ProxyType);
        }

        public void Run(string[] args)
        {
            var postId = args.Last();

            int.TryParse(args.SkipWhile(x => x != "-c").Take(2).Last(), out int countComments);

            var filePath = args.SkipWhile(x => x != "-f").Take(2).Last();

            if (countComments == 0)
            {
                Console.WriteLine("Ошибка синтаксиса");
                return;
            }

            if (countComments > _accounts.Count())
            {
                Console.WriteLine("Вы указали слишком большое кол-во лайков");
                return;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Такого файла не существует");
                return;
            }

            var comments = File.ReadAllLines(filePath);
            foreach (var account in _accounts)
            {
                if (countComments == 0) break;
                if (account.Authorization())
                {
                    if (account.Comment(postId, comments.GetRandom()))
                    {
                        countComments--;
                    }
                }
            }
        }
    }
}
