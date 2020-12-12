using System;
using System.IO;

namespace Gamania.OmniLogger
{
    public static class Logger
    {
        private static string logpath = "logs/latest.log";

        public static void WriteLine(string text)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                string message = ("[" + DateTime.Now + "] [INFO] " + text);
                Console.WriteLine(message);
                Directory.CreateDirectory("logs");
                using (StreamWriter filelog = File.AppendText(logpath))
                {
                    filelog.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                WriteLine(e.ToString(), "error");
            }
        }

        public static void Write(string text)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                string message = ("[" + DateTime.Now + "] [INFO] " + text);
                Console.Write(message);
                Directory.CreateDirectory("logs");
                using (StreamWriter filelog = File.AppendText(logpath))
                {
                    filelog.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                WriteLine(e.ToString(), "error");
            }
        }

        public static void WriteLine(string text, string type)
        {
            try
            {
                if (type == "info")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    type = "INFO";
                }
                if (type == "error")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    type = "ERROR";
                }
                if (type == "warning")
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    type = "WARNING";
                }

                string message = ("[" + DateTime.Now + "] [" + type + "] " + text);
                Console.WriteLine(message);
                Directory.CreateDirectory("logs");
                using (StreamWriter filelog = File.AppendText(logpath))
                {
                    filelog.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                WriteLine(e.ToString(), "error");
            }
        }
    }
}