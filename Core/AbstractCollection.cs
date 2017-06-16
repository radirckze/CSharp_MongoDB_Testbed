using MyMongo.Common;
using MongoDB.Driver;
using System;


namespace MyMongo.Core {

    public abstract class AbstractCollection {

        public IMongoDatabase MongoDb  {get; set;}

        protected AbstractCollection(IMongoDatabase MongoDb) {
            if (MongoDb == null) {
                throw new ArgumentException();
            }
            
            this.MongoDb = MongoDb;
        }

    }

}