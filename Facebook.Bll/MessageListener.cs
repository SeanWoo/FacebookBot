using DryIoc;
using Facebook.Bll.Handlers;
using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Facebook.Bll
{
    public class MessageListener
    {
        private List<KeyValuePair<string, IMessageHandler>> _commands = new List<KeyValuePair<string, IMessageHandler>>();

        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public MessageListener(IResolverContext context)
        {
            CreateCommand("setProxy", new SetterProxyHandler(context));
            CreateCommand("setAccount", new SetterAccountHandler(context));
            CreateCommand("like", new LikeHandler(context));
            CreateCommand("comment", new CommentHandler(context));
        }

        public void CreateCommand(string nameCommand, IMessageHandler handler)
        {
            if(!_commands.Any(x => x.Key == nameCommand))
                _commands.Add(new KeyValuePair<string, IMessageHandler>(nameCommand, handler));
        }

        public void Start()
        {
            Console.Title = "FacebookBot";

            string command;
            while ((command = Console.ReadLine()) != "exit")
            {
                var splitedCommand = command.Split();
                var nameCommand = splitedCommand[0];
                var args = splitedCommand.Skip(1).ToArray();

                if (_commands.Any(x => x.Key == nameCommand))
                {
                    _commands.First(x => x.Key == nameCommand).Value.Run(args, _tokenSource.Token);
                }
                else if (command == "q")
                {
                    _tokenSource.Cancel();
                    _tokenSource.Dispose();
                    _tokenSource = new CancellationTokenSource();
                }
                else
                {
                    Console.WriteLine("Такой комманды нету");
                }
            } 
        }
    }
}
