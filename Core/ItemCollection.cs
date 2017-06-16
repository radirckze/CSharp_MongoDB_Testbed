using MyMongo.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;


namespace MyMongo.Core {

    public class ItemCollection: AbstractCollection {

         private IMongoCollection<Item> itemCollection = null;

        public ItemCollection(IMongoDatabase mongoDb) : base(mongoDb) {

            //no checks done ... but its just test code
            itemCollection = MongoDb.GetCollection<Item>("Items");
        }

        public void InsertItems(List<Item> items) {
            try {
                // null checks, etc.
                itemCollection.InsertMany(items);
            }
            catch (Exception ex) {
                //do someting
                throw ex;
            }
        }

        public Item GetItem(ObjectId itemId) {
            try {
                Item theItem = null;
                IList<Item>  result = itemCollection.FindSync<Item>(
                               new BsonDocument("_id", itemId)).ToList();
                if (result != null && result.Count > 0)  {
                    theItem = result[0];
                }

                return theItem;

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<Item> GetItemsforMember(ObjectId memId) {
            try {
                List<Item>  result = itemCollection.FindSync<Item>(
                               new BsonDocument("AccessGroup", memId)).ToList();
                return result;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

    } // end ItemCollection
}