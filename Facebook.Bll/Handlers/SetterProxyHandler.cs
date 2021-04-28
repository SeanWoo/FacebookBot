﻿using DryIoc;
using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facebook.Bll.Handlers
{
    public class SetterProxyHandler : IMessageHandler
    {
        private ILogger _logger;

        public SetterProxyHandler(IResolverContext context)
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

            File.Copy(filePath, SharedData.PATH_TO_PROXY, true);
            _logger.Log("Успешно скопировано");
        }
    }
}
