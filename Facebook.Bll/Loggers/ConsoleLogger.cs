using Facebook.Shared.Interfaces;
using System;

namespace Facebook.Bll.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
