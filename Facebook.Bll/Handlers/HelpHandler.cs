using DryIoc;
using Facebook.Shared.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Facebook.Bll.Handlers
{
    public class HelpHandler : IMessageHandler
    {
        private ILogger _logger;

        public HelpHandler(IResolverContext context)
        {
            _logger = context.Resolve<ILogger>();
        }

        public async Task Run(string[] args, CancellationToken cancellationToken)
        {
            _logger.Log("Команды: \n" +
                        "   setProxy - устанавливает файл с прокси\n" +
                        "       Использование:\n" +
                        "           setProxy PATH_TO_FILE\n" +
                        "       Пример:\n" +
                        "           setProxy C:/proxy.txt\n" +
                        "\n" +
                        "\n" +
                        "   setAccount - устанавливает файл с аккаунтами (файл должен быть с расширением json)\n" +
                        "       Использование:\n" +
                        "           setAccount PATH_TO_FILE\n" +
                        "       Пример:\n" +
                        "           setProxy C:/account.json\n" +
                        "\n" +
                        "\n" +
                        "   like - накручивает лайки на нужный пост\n" +
                        "       Использование:\n" +
                        "           like -c COUNT [--proxy-disable] ID_POST\n" +
                        "\n" +
                        "           -c COUNT - количество лайков\n" +
                        "           --proxy-disable - выключить использование прокси\n" +
                        "       Пример:\n" +
                        "           like -с 15 18543654365\n" +
                        "           like -с 15 --proxy-disable 18543654365\n" +
                        "\n" +
                        "\n" +
                        "   comment - накручивает комменты на нужный пост\n" +
                        "       Использование:\n" +
                        "           comment [-c COUNT]|[-s] [--proxy-disable] -f PATH_TO_FILE ID_POST\n" +
                        "\n" +
                        "           -c COUNT - количество лайков\n" +
                        "           -s - накрутка на стрим (если указан этот ключ то можно не использова ключ -c)\n" +
                        "           -f PATH_TO_FILE - файл с коментариями (на каждой строчке новый комментарий)\n" +
                        "           --proxy-disable - выключить использование прокси\n" +
                        "       Пример:\n" +
                        "           comment -c 25 -f C:/texts.txt 18543654365\n" +
                        "           comment -s -f C:/texts.txt 18543654365\n" +
                        "           comment -c 25 --proxy-disable -f C:/texts.txt 18543654365\n" +
                        "\n");
        }
    }
}
