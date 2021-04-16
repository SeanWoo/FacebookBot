using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Facebook.CLI
{
    class MessageHandler
    {
        public bool TryExecuteCommand(string nameCommand, string input, Action<string[]> action)
        {
            var args = input.Split();
            if (args.Length > 0 && args[0] == nameCommand)
            {
                args = args.Skip(1).ToArray();
                action(args);
                return true;
            }
            return false;
        }

        public void Start()
        {
            string command;
            while ((command = Console.ReadLine()) != "exit")
            {
                var result = false;

                result |= TryExecuteCommand("like", command, (args) =>
                {
                    Console.WriteLine(args[0]);
                });
                result |= TryExecuteCommand("likeComment", command, (args) =>
                {
                    Console.WriteLine(args[0]);
                });
                result |= TryExecuteCommand("comment", command, (args) =>
                {
                    Console.WriteLine(args[0]);
                });

                if (!result)
                    Console.WriteLine("Такой комманды нету");
            }
        }
    }
}
