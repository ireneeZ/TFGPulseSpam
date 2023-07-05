using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PulseSpamApi.Models
{
    public class Pregunta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("pregunta")]
        public string PreguntaTxt { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("categoria_id")]
        public string CategoriaId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("tipo_id")]
        public string TipoId { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Pregunta pregunta &&
                   Id == pregunta.Id &&
                   PreguntaTxt == pregunta.PreguntaTxt &&
                   CategoriaId == pregunta.CategoriaId &&
                   TipoId == pregunta.TipoId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, PreguntaTxt, CategoriaId, TipoId);
        }
    }
}
