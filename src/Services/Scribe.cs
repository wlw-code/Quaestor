using System;

namespace Quaestor.Services
{
    public class Scribe
    {
        public void InformOfException(string errorMessage, Exception exception)
        {
            CreateOutput("Exception", $"{errorMessage}\n\n{exception}", ConsoleColor.Red);
        }

        public void Inform(string message)
        {
            CreateOutput("Notification", message);
        }

        private static void CreateOutput(string prefix, string finalMessage, ConsoleColor color = ConsoleColor.Blue)
        {
            Console.ForegroundColor = color;
            Console.Write($"[{prefix} at {DateTime.Now.ToShortTimeString()}] ");
            Console.ResetColor();
            Console.WriteLine(finalMessage);
        }
    }
}
