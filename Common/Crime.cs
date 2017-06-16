using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MyMongo.Common {

    public class Crime {

        public enum OffenceType {Arson, Assualt, Battery, Blackmail, Extortion, Fraud, Larceny, Theft};
        public enum DayOfWeek {Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday};

        public ObjectId _id {get; set;}

        public OffenceType Offence {get; set;}

        public DayOfWeek DayOfCrime {get; set; }

        public String City {get; set;}

        public override string ToString() {
            return String.Format("Id: {0}, Offence: {1}, Day: {2}, City: {3}",
            _id.ToString(), Offence, DayOfCrime, City);
        }

        public static void InitCrimesCollection(IMongoDatabase mongoDb) {
            IMongoCollection<Crime> crimesCollection = mongoDb.GetCollection<Crime>("Crimes");
            crimesCollection.InsertMany(GetInitData());
        }

        private static List<Crime> GetInitData() {
            Random rand = new Random();
            OffenceType offence;
            int offenceCount = Enum.GetNames(typeof(OffenceType)).Length;
            DayOfWeek day;
            int dayCount = Enum.GetNames(typeof(DayOfWeek)).Length;
            String city = "Los Angeles";
            List<Crime> crimes = new List<Crime>();

            for (int i=0; i<250; i++) {
                offence = (OffenceType)rand.Next(0, offenceCount);
                day = (DayOfWeek)rand.Next(0, dayCount);
                crimes.Add(new Crime() {Offence=offence, DayOfCrime=day, City= city});
            }

            return crimes;
        }

        public static void MapReduceOptions() {

            //https://www.youtube.com/watch?v=W-WihPoEbR4
            //https://github.com/sedouard/mongodb-mva

            //db.Crimes.mapReduce( 
            //    function() { emit(this.DayOfCrime, 1); }, 
            //    function(key, values) {return Array.sum(values); }, 
            //    { out: "crime_day_frequencies"} );

        }

    }
}