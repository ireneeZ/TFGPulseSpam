using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PulseSpamApi.Models
{
    public class TareaProgramacion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("fecha_inicio")]
        public DateTime FechaInicio { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("fecha_fin")]
        public DateTime FechaFin { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("preguntas_id")]
        public string[] PreguntasId { get; set; }

        [BsonElement("hora")]
        public string Hora { get; set; }
    }
}
