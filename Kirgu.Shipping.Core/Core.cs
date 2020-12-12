using Gamania.OmniLogger;
using Kirgu.Shipping.API;
using System;
using System.Net;
using System.Threading;

namespace Kirgu.Shipping.Core
{
    public static class Core
    {
        private static void Main(string[] args)
        {
            //------------------------
            ServicePointManager.DefaultConnectionLimit = 1024;
            ThreadPool.SetMinThreads(128, 256);
            ThreadPool.SetMaxThreads(128, 256);
            //------------------------

            Logger.WriteLine("Kirgu Shipping initializing...");
            Logger.WriteLine("Initializing HTTP API...");
            RunRestApi();
            Logger.WriteLine("Initializing HTTP API... [OK]");
        }

        public static void RunRestApi()
        {
            try
            {
                var RestApiThread = new Thread(() => HttpServer.API_Main());
                RestApiThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}