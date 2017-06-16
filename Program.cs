using MyMongo.Common;
using MyMongo.Core;
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
            IMongoDatabase mongoDb = client.GetDatabase("SampleDb1");

            //PART 1: Testing Crimes and MapReduce ... 
            bool initCrimesDb = false; //set to true for the first run
            CrimesCollection crimesCollection = new CrimesCollection(mongoDb);
            if (initCrimesDb) {
                crimesCollection.InsertCrimeData(GetCrimesInitData());
            }
            List<BsonDocument> results = crimesCollection.MapReduceCrimesByDay();
            PrintMapReduceResults(results);

            //PART 2: Testing cross referenced docs (see Items) ...
            // 2-A members ...
            bool initMembers = false; //set to true for first run
            MembersCollection memberCollection = new MembersCollection(mongoDb);        
            if (initMembers) {
                memberCollection.InsertMembers(GetMemberInitData());
            }

            List<Member> members = memberCollection.GetMembers();
            PrintMembers(members);

            Member mia = memberCollection.GetMemberByFirstName("Mia");
            PrintMembers(new List<Member>() {mia});

            //PART 2-B: Items ....
            bool initItems = false;
            ItemCollection itemCollection = new ItemCollection(mongoDb);
            if (initItems) {
                itemCollection.InsertItems(GetItemInitData(memberCollection));
            }
            
            mia = memberCollection.GetMemberByFirstName("Mia");
            Member marsellus = memberCollection.GetMemberByFirstName("Marsellus");
            List<Item> items = null;
            //Lets get the items that Marsellus can see. Should return 1
            items = itemCollection.GetItemsforMember(mia._id);
            PrintItemCollection(items, members);

            //Lets get items that Marsellus can see. Should return 2
            items = itemCollection.GetItemsforMember(marsellus._id);
            PrintItemCollection(items, members);

            Console.WriteLine("MongoDB test ... Done!");         

        }

        // Utility / debug functions ...

        public static void PrintMapReduceResults(List<BsonDocument> results) {

            if (results != null && results.Count > 0) {
                Console.WriteLine("\nPrinting results from map reduce on crimes ...");
                foreach(BsonDocument bsonDoc in results) {
                    //IEnumerable<BsonElement> elements = bsonDoc.Elements;
                    Console.WriteLine("Number of crimes on {0} is: {1}",
                    Enum.GetName(typeof(Crime.WeekDay), Convert.ToInt32(bsonDoc.GetElement("_id").Value)), 
                    bsonDoc.GetElement("value").Value); 
                }
            }

        }
        
        public static void PrintMembers(List<Member> members) {
            Console.WriteLine("\nMembers ...");
            if (members != null) {
                foreach(Member member in members) {
                    
                    Console.WriteLine(member.ToString());
                }
            }
        }

        private static List<Crime> GetCrimesInitData() {
            Random rand = new Random();
            Crime.OffenceType offence;
            int offenceCount = Enum.GetNames(typeof(Crime.OffenceType)).Length;
            Crime.WeekDay day;
            int dayCount = Enum.GetNames(typeof(DayOfWeek)).Length;
            String city = "Los Angeles";
            List<Crime> crimes = new List<Crime>();

            for (int i=0; i<250; i++) {
                offence = (Crime.OffenceType)rand.Next(0, offenceCount);
                day = (Crime.WeekDay)rand.Next(0, dayCount);
                crimes.Add(new Crime() {Offence=offence, DayOfCrime=day, City= city});
            }

            return crimes;
        }

        //method provides sample data
        public static List<Member> GetMemberInitData() {
            List<Member> members = new List<Member>();
            members.Add(new Member("Mia", "Wallace"));
            members.Add(new Member("Vincent", "Vega"));
            members.Add(new Member("Jules", "Winnfield"));
            members.Add(new Member("Marsellus", "Wallace"));
            members.Add(new Member("Butch", "Coolidge"));
            
            return members;
        }

        public static  List<Item> GetItemInitData(MembersCollection memberCollection) {
            List<Item> items = new List<Item>();
            Member mia = memberCollection.GetMemberByFirstName("Mia");
            Member marsellus = memberCollection.GetMemberByFirstName("Marsellus");
            Member jules = memberCollection.GetMemberByFirstName("Jules");
            Member vincent = memberCollection.GetMemberByFirstName("Vincent");

            //Mia create a document that Mia, Marsellus, Jules and Vincent have acces to
            items.Add(new Item(mia, "My funny story", new List<Member>() 
                     {mia, marsellus, jules, vincent}));
            
            //Marsellus create a document that Marsellus, Jules and Vincent have acces to
            items.Add(new Item(marsellus, "Why I threw Tony Rocky our of a window", new List<Member>() 
                     {marsellus, jules, vincent}));

            return items;
        }

        public static void PrintItemCollection(List<Item> items, List<Member> members) {
            Console.WriteLine("\n Items ...");
            if (items != null && items.Count > 0) {
                foreach(Item item in items) {
                    Console.WriteLine("ID: {0}, Owner={1}, Content={2}, AccessGroup={3}", item._id,
                    GetMemberNames(new List<ObjectId>() {item.OwnerId}, members), 
                    item.Content, GetMemberNames(item.AccessGroup, members));
                }
            }
        }

        private static string GetMemberNames(List<ObjectId> memIds, List<Member> members) {
            //do null checks ...
            string names = "";
            foreach(ObjectId objectId in memIds) {
                foreach(Member member in members) {
                    if (member._id == objectId) {
                        if (names.Length > 1) {
                            names = String.Concat(names, ", ");
                        }
                        names = String.Concat(names, member.FirstName);
                        break;
                    }
                }
            }
            
            return names;
        }

    } // end main
}
