using System;
using System.Collections.Generic;
using System.Text;

namespace Kirgu.Shipping.Config
{
    public static class Config
    {
        public const string MongoDB_Address = "mongodb+srv://" + MongoDB_Username + ":" + " + MongoDB_Password + " + "@cluster0.afxq4.mongodb.net/";
        public const string MongoDB_Username = "MongoKirguUser";
        public const string MongoDB_Password = "LAKRtLdNXekf8zuS";
        public const string Server_Socket_BindingAddress = "localhost";
        public const int Server_Socket_BindingPort = 8081;
        public const string Server_RestApi_BindingAddress = "localhost";
        public const int Server_RestApi_BindingPort = 8080;
        public const int Server_ConnectionLimit = 1024;
        public const int Server_Min_Worker_Threads = 128;
        public const int Server_Min_Worker_completionPortThreads = 256;
        public const int Server_Max_Worker_Threads = 128;
        public const int Server_Max_Worker_completionPortThreads = 256;
    }
}