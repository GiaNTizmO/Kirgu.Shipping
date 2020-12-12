using MongoDB.Driver;
using System;
using Kirgu.Shipping.API;

namespace Kirgu.Shipping.MongoDBManager
{
    public static class DBManager
    {
        private MongoClient client = new MongoClient(Config.MongoDB_Address);
        private IMongoDatabase database = client.GetDatabase("test");
    }
}