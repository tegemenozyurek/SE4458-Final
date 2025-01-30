using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Prescription_and_Doctor_Visit_Management_System.Models
{
    public class Medicine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public int Price { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
