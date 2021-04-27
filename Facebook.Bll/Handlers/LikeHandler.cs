using Facebook.CLI;
using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bll.Handlers
{
    public class LikeHandler : IMessageHandler
    {
        private IEnumerable<IAccount> _accounts;

        public LikeHandler(IFactory<IAccount> accounts, IProxyProvider provider)
        {
            _accounts = accounts.Get();

            provider.LoadProxy(SharedData.PATH_TO_PROXY, SettingsReader.ProxyType);
        }

        public void Run(string[] args)
        {
            var ids = args.Last().Split('/');

            var postId = ids.First();
            var commentId = ids.Length > 1 ? ids.Last() : null;

            int.TryParse(args.SkipWhile(x => x != "-c").Take(2).Last(), out int countLikes);

            if (countLikes == 0)
            {
                Console.WriteLine("Ошибка синтаксиса");
                return;
            }

            if (countLikes > _accounts.Count())
            {
                Console.WriteLine("Вы указали слишком большое кол-во лайков");
                return;
            }

            foreach (var account in _accounts)
            {
                if (countLikes == 0) break;
                if (account.Authorization())
                {
                    if (account.Like(postId, commentId))
                    {
                        countLikes--;
                    }
                }
            }
        }
    }
}
