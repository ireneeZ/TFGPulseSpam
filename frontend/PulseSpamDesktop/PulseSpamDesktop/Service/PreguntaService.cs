using PulseSpamDesktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PulseSpamDesktop.View;
using System.Windows;

namespace PulseSpamDesktop.Service
{
    public class PreguntaService
    {
        public bool ExecuteResponderCommand(Respuesta r, ScheduleTransfer actualSched)
        {
            Respuesta respuesta = PostRespuesta(r).Result;
            bool sUpd = false;
            if (respuesta != null)
            {
                Tracking t = new Tracking();
                t.UsuarioId = Properties.Settings.Default.Usuario;
                t.Fecha = DateTime.Now;

                bool tracking = PostTracking(t).Result;

                //Actualizar lista de respuestas
                Schedule s = new Schedule();
                s.FechaPregunta = actualSched.FechaPregunta;
                s.PreguntaId = actualSched.Pregunta.Id;
                s.RespuestasId.Add(respuesta.Id);
                sUpd = UpdateSchedule(actualSched.Id, s).Result;

                var currentWindow = Application.Current.MainWindow;
                var window = new YaRespondido();
                Application.Current.MainWindow = window;
                window.Show();
                currentWindow.Close();
            }
            return sUpd;
        }

        public void ExecuteOmitirCommand(Tracking t)
        {
            bool tracking = PostTracking(t).Result;

            var currentWindow = Application.Current.MainWindow;
            var window = new YaRespondido();
            Application.Current.MainWindow = window;
            window.Show();
            currentWindow.Close();
        }

        public ScheduleTransfer GetSchedule(string fecha)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiUrl = Urls.baseURI + Urls.getSchedule + "/" + fecha.ToString();
            HttpResponseMessage resp;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(900);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(apiUrl);
                response.Wait();
                resp = response.Result;
            }
            string content = resp.Content.ReadAsStringAsync().Result;
            ScheduleTransfer s = JsonConvert.DeserializeObject<ScheduleTransfer>(content);
            return s;
        }

        public Pregunta GetPregunta(string id)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiUrl = Urls.baseURI + Urls.getPregunta + "/" + id.ToString();
            HttpResponseMessage resp;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(900);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(apiUrl);
                response.Wait();
                resp = response.Result;
            }
            string content = resp.Content.ReadAsStringAsync().Result;
            Pregunta p = JsonConvert.DeserializeObject<Pregunta>(content);
            return p;
        }

        public async Task<Respuesta> PostRespuesta(Respuesta r)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiUrl = Urls.baseURI + Urls.postRespuesta;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(900);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(r);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                var response = client.PostAsync(apiUrl, content);
                ;
                response.Wait();

                if (response.Result.StatusCode.Equals(HttpStatusCode.Created))
                {
                    string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                    Respuesta rPosted = JsonConvert.DeserializeObject<Respuesta>(contentResult);
                    return rPosted;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> PostTracking(Tracking t)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiUrl = Urls.baseURI + Urls.postTracking;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(900);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(t, Formatting.Indented);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                var response = client.PostAsync(apiUrl, content);

                response.Wait();

                if (response.Result.StatusCode.Equals(HttpStatusCode.Created))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateSchedule(string id, Schedule s)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiUrl = Urls.baseURI + Urls.getSchedule + "/" + id;
            HttpResponseMessage resp;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(900);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(s, Formatting.Indented);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                var response = client.PutAsync(apiUrl, content);

                response.Wait();
                resp = response.Result;

                if (response.Result.StatusCode.Equals(HttpStatusCode.NoContent))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
