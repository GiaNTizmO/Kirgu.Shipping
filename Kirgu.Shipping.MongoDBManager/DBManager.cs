using MongoDB.Driver;
using System;
using Kirgu.Shipping.Config;

namespace Kirgu.Shipping.MongoDBManager
{
    public static class DBManager
    {
        private MongoClient client = new MongoClient(Config.Config.MongoDB_Address);
        private IMongoDatabase database = client.GetDatabase("test");
    }
}