using MongoDB.Driver;
using System;
using Kirgu.Shipping.Config;
using System.Threading.Tasks;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using MongoDB.Bson.IO;

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

        public static int AuthProcessor(string username, string password)
        {
            var find = new BsonDocument { { "login", username }
        };
            var db = client.GetDatabase("Kirgu");
            var collection = db.GetCollection<BsonDocument>("accounts");

            var resultFind = collection.Find(find).ToList();
            long count = collection.Count(find);
            Console.WriteLine(count);
            if (count > 0)
            {
                var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                JObject json = JObject.Parse(resultFind[0].ToJson<MongoDB.Bson.BsonDocument>(jsonWriterSettings));

                Console.WriteLine(json);
                dynamic authJSON = Newtonsoft.Json.JsonConvert.DeserializeObject(json.ToString());
                Console.WriteLine(authJSON.password);
                if (authJSON.password == password)
                    return 2; //VALID USER
                else
                    return 1; //INCORRECT PASSWORD
            }
            else
                return 0; //USER NOT FOUND
            return -1; //SERVER ERROR
        }
    }
}