using MyMongo.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;


namespace MyMongo.Core {

    public class MembersCollection : AbstractCollection {

        //The first names of the initial members, just in case I need them
        public static List<string> firstName = new List<string>() {"Mia", "Vincent", "Jules",
            "Marsellus", "Butch"};

        private IMongoCollection<Member> memCollection = null;

        public MembersCollection(IMongoDatabase mongoDb) : base(mongoDb) {

            //no checks done ... but its just test code
            memCollection = MongoDb.GetCollection<Member>("Members");
        }

        public void InsertMembers(List<Member> members) {
            try {
            //validate that members is not null, and that each member is valid.
                memCollection.InsertMany(members);
            }
             catch (Exception ex) {
                //do someting.
                throw ex;
            }
        }

        public Member GetMemberByFirstName(string firstName) {
            try {
                Member theMember = null;
                IList<Member>  result = memCollection.FindSync<Member>(
                               new BsonDocument("FirstName", firstName)).ToList();
                if (result != null && result.Count > 0)  {
                    theMember = result[0];
                }

                return theMember;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<Member> GetMembers() {
            try {
               //return memCollection.Aggregate().ToList();
                return memCollection.FindSync<Member>(new BsonDocument()).ToList();
            }
             catch (Exception ex) {
                //do someting.
                throw ex;
            }
        }
        
    }

}