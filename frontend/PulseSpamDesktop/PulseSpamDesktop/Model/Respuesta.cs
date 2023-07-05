using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseSpamDesktop.Model
{
    public class Respuesta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("tipo")]
        public string Tipo { get; set; }

        [BsonElement("respuesta_libre")]
        public string? RespuestaLibre { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        [BsonElement("respuesta_sino")]
        public bool? RespuestaSiNo { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        [BsonElement("respuesta_puntuacion")]
        public int? RespuestaPuntuacion { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("fecha_pregunta")]
        public DateTime Fecha { get; set; }

        /*[BsonElement("genero")]
        public string Genero { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [BsonElement("antiguedad_meses")]
        public int AntiguedadMeses { get; set; }*/

        [BsonElement("usuario_id")]
        public string? UsuarioId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("pregunta_id")]
        public string? PreguntaId { get; set; }
    }
}
