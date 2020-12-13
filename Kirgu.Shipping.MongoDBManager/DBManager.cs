using MongoDB.Driver;
using System;
using Kirgu.Shipping.Config;
using System.Threading.Tasks;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using MongoDB.Bson.IO;
using System.Net;
using System.Text;

namespace Kirgu.Shipping.MongoDBManager
{
    public class DBManager
    {
        public static MongoClient client = new MongoClient("mongodb+srv://MongoKirguUser:LAKRtLdNXekf8zuS@cluster0.afxq4.mongodb.net/");
        public static string HardJsonValue = "";

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

        public static int AuthRecoveryProcessor(string username, string phone)
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
                if (authJSON.phone == phone)
                {
                    string newpasswd = CreatePassword(8);
                    string url = "https://smsc.ru/sys/send.php?login=<login>&psw=<password>&phones=" + authJSON.phone + "&mes=Новый пароль от аккаунта Киргу:";
                    using (var webClient = new WebClient())
                    {
                        var response = webClient.DownloadString(url);
                    }
                    Console.WriteLine(newpasswd);
                    var filter = Builders<BsonDocument>.Filter.Eq("login", username);
                    var update = Builders<BsonDocument>.Update.Set("password", newpasswd);
                    var result = collection.UpdateOne(filter, update);
                    return 2; //VALID USER
                }
                else
                    return 1; //INCORRECT PHONE
            }
            else
                return 0; //USER NOT FOUND
            return -1; //SERVER ERROR
        }

        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string GetOrders(string username)
        {
            var find = new BsonDocument { { "accountID", ObjectId(GetAccountOID(username)) }
        };
            var db = client.GetDatabase("Kirgu");
            var collection = db.GetCollection<BsonDocument>("orders");

            var resultFind = collection.Find(find).ToList();
            long count = collection.Count(find);
            Console.WriteLine(count);
            if (count > 0)
            {
                var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                JObject json = JObject.Parse(resultFind[0].ToJson<MongoDB.Bson.BsonDocument>(jsonWriterSettings));

                Console.WriteLine("LOL1: " + json);
                JObject authJSON = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(json.ToString());
                string Account_ObjectId = authJSON["_id"].Value<string>("$oid");
                Console.WriteLine("LOL2: " + Account_ObjectId.ToString());
            }
            else
                return 0; //USER NOT FOUND
            return -1; //SERVER ERROR
        }

        public static string GetAccountOID(string username)
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
                JObject authJSON = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(json.ToString());
                string Account_ObjectId = authJSON["_id"].Value<string>("$oid");
                return Account_ObjectId;
            }
            else
                return null; //SERVER ERROR
        }
    }
}