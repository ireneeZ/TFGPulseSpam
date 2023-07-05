﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseSpamDesktop.Model
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
        public List<string> RespuestasId { get; set; } = new List<string>();
    }
}
