using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PulseSpamApi.Models
{
    public class Tracking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("usuario_id")]
        public string UsuarioId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("fecha")]
        public DateTime Fecha { get; set; }
    }
}
