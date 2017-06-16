using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MyMongo.Common {

    public class Crime {

        public enum OffenceType {Arson, Assualt, Battery, Blackmail, Extortion, Fraud, Larceny, Theft};
        public enum WeekDay {Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday};

        public ObjectId _id {get; set;}

        public OffenceType Offence {get; set;}

        public WeekDay DayOfCrime {get; set; }

        public String City {get; set;}

        public override string ToString() {
            return String.Format("Id: {0}, Offence: {1}, Day: {2}, City: {3}",
            _id.ToString(), Offence, DayOfCrime, City);
        }

    }
}