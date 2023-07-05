using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseSpamDesktop.Model
{
    public class Tracking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("usuario")]
        public string UsuarioId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("fecha")]
        public DateTime Fecha { get; set; }
    }
}
