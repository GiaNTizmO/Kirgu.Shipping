using Gamania.OmniLogger;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kirgu.Shipping.API
{
    public static class HttpServer
    {
        public static string[] credentials = new string[2];
        public static HttpListener listener;
        public static string url = "http://" + Config.Config.Server_RestApi_BindingAddress + ":" + Config.Config.Server_RestApi_BindingPort + "/";
        public static int pageViews = 0;
        public static int requestCount = 0;

        //public static string pageRaw = "{\"Status\"}";
        public static string pageData = "false";

        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

#if Debug
                // Print out some info about the request
                Console.WriteLine("Request #: {0}", ++requestCount);
                Console.WriteLine(req.Url.ToString());
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);
                Console.WriteLine();

                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                {
                    Console.WriteLine("Shutdown requested");
                    runServer = false;
                }
#endif

                if ((req.HttpMethod == "GET") && (req.Url.AbsolutePath.Contains("/auth=")))
                {
                    Logger.WriteLine("Auth packet recieved!");
                    Console.WriteLine(req.Url.AbsolutePath);
                    AuthParser(req.Url.AbsolutePath);
                    Console.WriteLine(credentials[0] + credentials[1]);
                }

                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }

        public static void API_Main()
        {
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }

        public static string[] AuthParser(string authStr)
        {
            string[] Raw = authStr.Split('&');
            string[] Login = Raw[0].Split('=');
            string[] Password = Raw[1].Split('=');
            credentials[0] = Login[1];
            credentials[1] = Password[1];
            return credentials;
        }
    }
}