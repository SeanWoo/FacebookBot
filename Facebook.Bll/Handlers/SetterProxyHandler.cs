using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bll.Handlers
{
    public class SetterProxyHandler : IMessageHandler
    {
        public void Run(string[] args)
        {
            var filePath = args[0];

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Такого файла не существует");
                return;
            }
            File.Copy(filePath, SharedData.PATH_TO_PROXY, true);
        }
    }
}
