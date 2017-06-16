using MyMongo.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMongo {

    class Program {


        static void Main(string[] args) {
            
            string connectionString = "mongodb://localhost:27017";
            MongoDB.Driver.MongoClient client = new MongoClient(connectionString);
            IMongoDatabase mongoDb = client.GetDatabase("sampledb1");

            //IMongoCollection<BsonDocument> memBdCollection = mongoDb.GetCollection<BsonDocument>("members");
            //InsertMemberBson(memBdCollection);

            List<Member> members = null;
            IMongoCollection<Member> memCollection = mongoDb.GetCollection<Member>("members");
            //members = new List<Member>() {new Member() {firstname="Jack", lastname="Doe"}};
            //memCollection.InsertMany(members);

            //members = memCollection.Aggregate().ToList();
            //PrintMembers(members);

            Crime.InitCrimesCollection(mongoDb);


            Console.WriteLine("Done!");         

        }

        public static void InsertMemberBson(IMongoCollection<BsonDocument> memCollection) {
            var document = new BsonDocument {
                {"firstname", BsonValue.Create("Jon")},
                {"lastname", new BsonString("Doe")},
            };
            memCollection.InsertOne(document);
        }
        
        public static void PrintMembers(List<Member> members) {
            Console.WriteLine("\nMembers ...");
            if (members != null) {
                foreach(Member member in members) {
                    Console.WriteLine(member.ToString());
                }
            }
        }

        

       

    }
}
