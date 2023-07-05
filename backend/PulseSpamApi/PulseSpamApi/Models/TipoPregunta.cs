using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PulseSpamApi.Models
{
    public class TipoPregunta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("tipo")]
        public string Tipo { get; set; }
    }
}
