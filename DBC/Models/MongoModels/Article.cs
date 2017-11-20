using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DBC.Models.MongoModels
{
    public class Article
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("PublicationDate")]
        public DateTime PublicationDate { get; set; }
        [BsonElement("Url")]
        public string Url { get; set; }
    }
}