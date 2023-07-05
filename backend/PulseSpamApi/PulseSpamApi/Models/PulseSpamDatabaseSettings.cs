using System;

namespace PulseSpamApi.Models
//namespace IdentityMongo.Settings
{
    public class PulseSpamDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string PreguntaCollectionName { get; set; } = null!;

        public string RespuestaCollectionName { get; set; } = null!;

        public string UsuarioCollectionName { get; set; } = null!;

        public string CategoriaCollectionName { get; set; } = null!;

        public string DepartamentoCollectionName { get; set; } = null!;

        public string TrackingCollectionName { get; set; } = null!;

        public string SchedulingCollectionName { get; set; } = null!;

        public string TipoPreguntaCollectionName { get; set; } = null!;

        public string TareaProgramacionCollectionName { get; set; } = null!;
    }
}
