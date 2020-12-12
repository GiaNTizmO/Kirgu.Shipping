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

            var find = new BsonDocument { { "student_id", 10000 }
        };
            var db = client.GetDatabase("testDatabase");
            var collection = db.GetCollection<BsonDocument>("testCollection");
            collection.InsertOne(document);

            Console.WriteLine(collection.Find(document));
        }

        public static void FindCollection()
        {
        }
    }
}