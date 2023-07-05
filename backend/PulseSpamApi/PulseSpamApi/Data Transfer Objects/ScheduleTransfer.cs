using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PulseSpamApi.Models
{
    public class ScheduleTransfer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("pregunta")]
        public Pregunta Pregunta { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("fecha_pregunta")]
        public DateTime FechaPregunta { get; set; }

        [BsonElement("respuestas")]
        public List<Respuesta> Respuestas { get; set; }

        public ScheduleTransfer(Schedule s, Pregunta pregunta, List<Respuesta> respuestas)
        {
            Id = s.Id;
            Pregunta = pregunta;
            FechaPregunta = s.FechaPregunta;
            Respuestas = respuestas;
        }
    }
}
