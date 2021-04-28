using DryIoc;
using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Facebook.Bll.Handlers
{
    public class LikeHandler : IMessageHandler
    {
        private ILogger _logger;
        private IFactory<IAccount> _factory;
        private IProxyProvider _proxyProvider;

        private IEnumerable<IAccount> _accounts;

        public LikeHandler(IResolverContext context)
        {
            _logger = context.Resolve<ILogger>();
            _factory = context.Resolve<IFactory<IAccount>>();
            _proxyProvider = context.Resolve<IProxyProvider>();

            _accounts = _factory.Get();
            _proxyProvider.LoadProxy(SharedData.PATH_TO_PROXY, SettingsReader.ProxyType);
        }

        public async Task Run(string[] args, CancellationToken cancellationToken)
        {
            var ids = args.Last().Split(':');

            var postId = ids.First();
            var commentId = ids.Length > 1 ? ids.Last() : null;

            int.TryParse(args.SkipWhile(x => x != "-c").Take(2).Last(), out int countLikes);
            var isProxyDisable = args.Any(x => x == "--proxy-disable");

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
            await Task.Run(() =>
            {
                foreach (var account in _accounts)
                {
                    if (countLikes == 0 || cancellationToken.IsCancellationRequested) break;

                    if (isProxyDisable)
                        account.EnableProxies = false;

                    if (account.Authorization())
                    {
                        if (account.Like(postId, commentId))
                        {
                            countLikes--;
                            _logger.Log($"Осталось лайков: {countLikes}");
                        }
                    }
                }
            }, cancellationToken);
            _logger.Log($"Завершено");
        }
    }
}
