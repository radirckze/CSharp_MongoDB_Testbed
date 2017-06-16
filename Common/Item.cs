using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MyMongo.Common {

    public class Item {

        //... creating a member list just so I don't need to create a member table for this test
        public enum MemName {Mia, Vincent, Jules, Marsellus, Butch}; 
        public static Dictionary<MemName, string> Members = new Dictionary<MemName, string> ()
            {{MemName.Mia, "Mia"}, {MemName.Vincent, "Vincent"}, {MemName.Jules, "Jules"}, 
            {MemName.Marsellus, "Marsellus"}, {MemName.Jules, "Jules"} };

        public ObjectId _id { get; set; }

        //owner of the item
        public ObjectId OwnerId {get; set;}

        //some content
        public string Content {get; set;}

        // members who have access to the item
        public List<ObjectId> AccessGroup { get; set; }

        //empty ctor is required for MondgoDb.
        public Item() {}
 
        // should have kept this test simple, and just used hard coded user_id / name pairs
        public Item(Member owner, string content, List<Member> accessGroup) {
            if (owner == null || String.IsNullOrWhiteSpace(content) 
                || accessGroup == null || accessGroup.Count < 0) {
                    throw new ArgumentException();
            }

            this.OwnerId = owner._id;
            this.Content = content;
            // note, if the owner is not in the accessGroup, the owner has no access ...
            AccessGroup = new List<ObjectId>();
            foreach(Member member in accessGroup) {
                AccessGroup.Add(member._id);
            }
        }

    }

}