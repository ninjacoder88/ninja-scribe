using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NinjaScribe.DataAccess
{
    [BsonIgnoreExtraElements]
    public class Visit
    {
        public ObjectId _id { get; set; }

        public string RemoteIpAddress { get; set; }

        public string LocalIpAddress { get; set; }

        public string UserAgent { get; set; }

        public string Location { get; set; }

        public DateTimeOffset VisitTime { get; set; }

        public string VisitTimeString { get; set; }
    }
}
