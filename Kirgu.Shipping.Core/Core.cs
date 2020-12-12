﻿using Gamania.OmniLogger;
using Kirgu.Shipping.API;
using Kirgu.Shipping.Config;
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
            ServicePointManager.DefaultConnectionLimit = Config.Config.Server_ConnectionLimit;
            ThreadPool.SetMinThreads(Config.Config.Server_Min_Worker_Threads, Config.Config.Server_Min_Worker_completionPortThreads);
            ThreadPool.SetMaxThreads(Config.Config.Server_Max_Worker_Threads, Config.Config.Server_Max_Worker_completionPortThreads);
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