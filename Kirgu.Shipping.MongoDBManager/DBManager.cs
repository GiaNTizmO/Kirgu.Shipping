using MongoDB.Driver;
using System;
using Kirgu.Shipping.Config;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Kirgu.Shipping.MongoDBManager
{
    public class DBManager
    {
        public static MongoClient client = new MongoClient("mongodb+srv://MongoKirguUser:LAKRtLdNXekf8zuS@cluster0.afxq4.mongodb.net/");

        public static void Test()
        {
            var document = new BsonDocument { { "student_id", 10000 }, {
                "scores",
                new BsonArray {
                new BsonDocument { { "type", "exam" }, { "score", 88.12334193287023 } },
                new BsonDocument { { "type", "quiz" }, { "score", 74.92381029342834 } },
                new BsonDocument { { "type", "homework" }, { "score", 89.97929384290324 } },
                new BsonDocument { { "type", "homework" }, { "score", 82.12931030513218 } }
                }
                }, { "class_id", 480 }
        };

            var find = new BsonDocument { { "orderId", "Z-201207" }
        };

            var db = client.GetDatabase("Kirgu");
            var collection = db.GetCollection<BsonDocument>("orders");
            //collection.InsertOne(document);
            var resultFind = collection.Find(find).ToList();
            Console.WriteLine(resultFind[0]);
        }

        public static string FindCollection(string row, string value, string CollectionName, string DatabaseName)
        {
            var db = client.GetDatabase(DatabaseName);
            var collection = db.GetCollection<BsonDocument>(CollectionName);
            var find = new BsonDocument { { "orderId", "Z-201207" } };
            return collection.Find(find).ToList().ToString();
        }

        public static string FindCollection(string row, int value, string CollectionName, string DatabaseName)
        {
            var db = client.GetDatabase(DatabaseName);
            var collection = db.GetCollection<BsonDocument>(CollectionName);
            var find = new BsonDocument { { row, value } };
            return collection.Find(find).ToList().ToJson();
        }
    }
}