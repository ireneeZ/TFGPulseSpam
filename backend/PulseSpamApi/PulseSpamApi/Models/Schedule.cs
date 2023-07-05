using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PulseSpamApi.Models
{
    public class Schedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("pregunta_id")]
        public string PreguntaId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("fecha_pregunta")]
        public DateTime FechaPregunta { get; set; }

        [BsonElement("respuestas_id")]
        public List<string> RespuestasId { get; set; }
    }
}
