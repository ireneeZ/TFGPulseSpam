using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PulseSpamApi.Models
{
    public class Categoria
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("categoria")]
        public string CategoriaCat { get; set; }
    }
}
