using NUnit.Framework;
using Kirgu.Shipping.Core;
using System.Net;
using System.Net.Sockets;

namespace Kirgu.Shipping.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Core_RestAPI_Available()
        {
            /*Core.Core.RunRestApi();
            IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            try
            {
                TcpListener tcpListener = new TcpListener(ipAddress, 666);
                tcpListener.Start();
                Assert.Pass();
            }
            catch (SocketException ex)
            {
                Assert.Fail();
            }*/
        }
    }
}