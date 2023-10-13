using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.Net6.Model.Entities
{
    public abstract class BaseDocument
    {
        public BaseDocument()
        {
            CreateDate = DateTime.UtcNow;
        }

        [BsonId]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string? Id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("createDate")]
        public DateTime CreateDate { get; set; }
        [BsonElement("createBy")]
        public string? CreateBy { get; set; } = "Scalante";
    }
}
