using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PulseSpamDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseSpamDesktop.Model
{
    public class Pregunta
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("pregunta_id")]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("categoria_id")]
        public string? CategoriaId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("tipo_id")]
        public string? TipoId { get; set; }

        [BsonElement("pregunta")]
        public string PreguntaTxt { get; set; }

        [BsonElement("categoria")]
        public string CategoriaCat { get; set; }

        [BsonElement("tipo")]
        public string Tipo { get; set; }
    }
}
