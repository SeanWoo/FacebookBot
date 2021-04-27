using DryIoc;
using Facebook.Bll.Handlers;
using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Bll
{
    public class MessageListener
    {
        private List<KeyValuePair<string, IMessageHandler>> _commands = new List<KeyValuePair<string, IMessageHandler>>();

        public MessageListener(IResolverContext context)
        {
            CreateCommand("setProxy", new SetterProxyHandler());
            CreateCommand("setAccount", new SetterAccountHandler());
            CreateCommand("like", new LikeHandler(context.Resolve<IFactory<IAccount>>(), context.Resolve<IProxyProvider>()));
            CreateCommand("comment", new CommentHandler(context.Resolve<IFactory<IAccount>>(), context.Resolve<IProxyProvider>()));
        }

        public void CreateCommand(string nameCommand, IMessageHandler handler)
        {
            if(!_commands.Any(x => x.Key == nameCommand))
                _commands.Add(new KeyValuePair<string, IMessageHandler>(nameCommand, handler));
        }

        public void Start()
        {
            string command;
            while ((command = Console.ReadLine()) != "exit")
            {
                var splitedCommand = command.Split();
                var nameCommand = splitedCommand[0];
                var args = splitedCommand.Skip(1).ToArray();

                if (_commands.Any(x => x.Key == nameCommand))
                {
                    _commands.First(x => x.Key == nameCommand).Value.Run(args);
                }
                else
                {
                    Console.WriteLine("Такой комманды нету");
                }
            } 
        }
    }
}
