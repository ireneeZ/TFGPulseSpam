using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PulseSpamApi.Models;

namespace PulseSpamApi.Data_Transfer_Objects
{
    public class PreguntaTransfer
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

        public PreguntaTransfer(Pregunta pregunta, Categoria cat, TipoPregunta tipo)
        {
            Id = pregunta.Id;
            CategoriaId = pregunta.CategoriaId;
            TipoId = pregunta.TipoId;
            PreguntaTxt = pregunta.PreguntaTxt;
            CategoriaCat = cat.CategoriaCat;
            Tipo = tipo.Tipo;
        }
    }
}
