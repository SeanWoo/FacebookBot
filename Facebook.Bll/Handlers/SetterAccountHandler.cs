using DryIoc;
using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Facebook.Bll.Handlers
{
    public class SetterAccountHandler : IMessageHandler
    {
        private ILogger _logger;

        public SetterAccountHandler(IResolverContext context)
        {
            _logger = context.Resolve<ILogger>();
        }

        public async Task Run(string[] args, CancellationToken cancellationToken)
        {
            var filePath = args[0];

            if (!File.Exists(filePath))
            {
                _logger.Log("Такого файла не существует");
                return;
            }

            File.Copy(filePath, SharedData.PATH_TO_ACCOUNT, true);
            _logger.Log("Успешно скопировано");
        }
    }
}
