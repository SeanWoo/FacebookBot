using DryIoc;
using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Facebook.Bll.Handlers
{
    public class AuthHandler : IMessageHandler
    {
        private ILogger _logger;
        private IProxyProvider _proxyProvider;

        public AuthHandler(IResolverContext context)
        {
            _logger = context.Resolve<ILogger>();
            _proxyProvider = context.Resolve<IProxyProvider>();

            _proxyProvider.LoadProxy(SharedData.PATH_TO_PROXY, SettingsReader.ProxyType);
        }

        public async Task Run(string[] args, CancellationToken cancellationToken)
        {
            await Task.Delay(1000);
            _logger.Log("Завершено");
        }
    }
}
