using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseSpamDesktop.Model
{
    public class Urls
    {
        public const string baseURI = "https://localhost:5001";

        public const string login = "/api/auth/login";

        public const string getPregunta = "/api/preguntas";
        public const string getSchedule = "/api/schedules";
        public const string getTracking = "/api/trackings";
        public const string getUsuario = "/api/usuarios";

        public const string postRespuesta = "/api/respuestas";
        public const string postTracking = "/api/trackings";
    }
}
