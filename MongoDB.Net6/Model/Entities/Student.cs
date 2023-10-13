using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.Net6.Model.Entities
{
    public class Student : BaseDocument
    {
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("age")]
        public int Age { get; set; }
    }
}
