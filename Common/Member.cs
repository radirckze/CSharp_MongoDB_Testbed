using MongoDB.Bson;
using System;

namespace MyMongo.Common {

    public class Member {

        public ObjectId _id {get; set;}

        public string firstname {get; set;}

        public string lastname {get; set;}

        public override string ToString() {
            return String.Format("Id: {0}, First: {1}, Last: {2}",_id.ToString(), firstname, lastname);
        }

    }
}