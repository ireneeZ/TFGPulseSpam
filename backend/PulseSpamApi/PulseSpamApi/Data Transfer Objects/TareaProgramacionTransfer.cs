using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PulseSpamApi.Models;
using MongoDB.Bson.IO;

namespace PulseSpamApi.Data_Transfer_Objects
{
    public class TareaProgramacionTransfer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("tarea_id")]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("fecha_inicio")]
        public DateTime FechaInicio { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("fecha_fin")]
        public DateTime FechaFin { get; set; }

        [BsonElement("hora")]
        public string Hora { get; set; }

        [BsonElement("preguntas")]
        public List<Pregunta> Preguntas { get; set; }

        public TareaProgramacionTransfer(TareaProgramacion tarea, List<Pregunta> preguntas)
        {
            Id = tarea.Id;
            FechaInicio = tarea.FechaInicio;
            FechaFin = tarea.FechaFin;
            Hora = tarea.Hora;
            Preguntas = preguntas;
        }
    }
}
