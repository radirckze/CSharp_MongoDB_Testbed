using MongoDB.Bson;
using System;

namespace MyMongo.Common {

    public class Member {

        public ObjectId _id {get; set;}

        public string FirstName {get; set;}

        public string LastName {get; set;}

        // default constructor required by the MongoDb driver
        public Member() {

        }

        public Member(string firstName, string lastName) {
            if (String.IsNullOrWhiteSpace(firstName) || String.IsNullOrWhiteSpace(lastName)) {
                throw new ArgumentException();
            }

            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public override string ToString() {
            return String.Format("Id: {0}, First: {1}, Last: {2}",_id.ToString(), FirstName, LastName);
        }

        public bool IsValid() {
            //check validity
            return true;
        }

    }
}