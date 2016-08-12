using System;

namespace SIS.Projector
{
    public class ConsoleLogger : ILog
    {
        public void Info(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }
    }
}
