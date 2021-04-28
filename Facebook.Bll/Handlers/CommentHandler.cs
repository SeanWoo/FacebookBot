using DryIoc;
using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using Facebook.Shared.Models.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Facebook.Bll.Handlers
{
    public class CommentHandler : IMessageHandler
    {
        private ILogger _logger;
        private IFactory<IAccount> _factory;
        private IProxyProvider _proxyProvider;

        private IEnumerable<IAccount> _accounts;

        public CommentHandler(IResolverContext context)
        {
            _logger = context.Resolve<ILogger>();
            _factory = context.Resolve<IFactory<IAccount>>();
            _proxyProvider = context.Resolve<IProxyProvider>();

            _accounts = _factory.Get();
            _proxyProvider.LoadProxy(SharedData.PATH_TO_PROXY, SettingsReader.ProxyType);
        }

        public async Task Run(string[] args, CancellationToken cancellationToken)
        {
            var postId = args.Last();

            var isStream = args.Any(x => x == "-s");
            var countComments = 0;
            if (!isStream)
                int.TryParse(args.SkipWhile(x => x != "-c").Take(2).Last(), out countComments);

            var isProxyDisable = args.Any(x => x == "--proxy-disable");
            var filePath = args.SkipWhile(x => x != "-f").Take(2).Last();

            if (countComments == 0 && !isStream)
            {
                _logger.Log("Вы не указали кол-во комментов");
                return;
            }

            if (!File.Exists(filePath))
            {
                _logger.Log("Такого файла не существует");
                return;
            }

            await Task.Run(() =>
            {
                var comments = File.ReadAllLines(filePath);
                do
                {
                    foreach (var account in _accounts)
                    {
                        if ((countComments == 0 && !isStream) || cancellationToken.IsCancellationRequested) break;

                        if (isProxyDisable)
                            account.EnableProxies = false;

                        if (account.Authorization())
                        {
                            if (isStream)
                            {
                                var text = comments.GetRandom();
                                if (account.CommentStream(postId, text))
                                {
                                    _logger.Log(text);
                                }
                            }
                            else
                            {
                                var text = comments.GetRandom();
                                if (account.Comment(postId, text))
                                {
                                    countComments--;
                                    _logger.Log(text);
                                }
                            }
                        }
                    }
                } while ((isStream || countComments != 0) && !cancellationToken.IsCancellationRequested);
            }, cancellationToken);
            _logger.Log($"Завершено");
        }
    }
}
