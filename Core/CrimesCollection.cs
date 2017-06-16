using MyMongo.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;


namespace MyMongo.Core {


    public class CrimesCollection : AbstractCollection {

         private IMongoCollection<Crime> crimesCollection = null;

        public CrimesCollection(IMongoDatabase mongoDb) : base(mongoDb) {
            crimesCollection = MongoDb.GetCollection<Crime>("Crimes");
        }

        // use this to initialize the database as well.
        public void InsertCrimeData(List<Crime> crimeData) {
            crimesCollection.InsertMany(crimeData);
        }

        //Note: MapReduce may take a while so ... production may need a different approach
        public List<BsonDocument> MapReduceCrimesByDay() {

            BsonJavaScript map  = new BsonJavaScript(@"
                  function() { emit(this.DayOfCrime, 1); }");
            BsonJavaScript reduce = new BsonJavaScript(@"
                   function(key, values) {return Array.sum(values); }");
            MapReduceOptions<Crime, BsonDocument> mro = new MapReduceOptions<Crime, BsonDocument>();
            mro.OutputOptions = MapReduceOutputOptions.Inline;

            List<BsonDocument> results = crimesCollection.MapReduce(map, reduce, mro).ToList();

            return results;
        }

    }

}