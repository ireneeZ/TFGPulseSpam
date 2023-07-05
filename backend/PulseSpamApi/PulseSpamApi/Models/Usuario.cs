using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Runtime.CompilerServices;

namespace PulseSpamApi.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("tipo")]
        public string Tipo { get; set; }

        [BsonElement("nombre")]
        public String Nombre { get; set; }

        [BsonElement("password")]
        public String Password { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("departamento_id")]
        public string DepartamentoId { get; set; }

    }
}
