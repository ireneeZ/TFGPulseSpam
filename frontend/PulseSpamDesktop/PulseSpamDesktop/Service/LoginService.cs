using PulseSpamDesktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json;
using PulseSpamDesktop.View;
using System.Windows;

namespace PulseSpamDesktop.Service
{
    public class LoginService
    {
        public string? ExecuteLoginCommand(UsuarioLogin u)
        {
            HttpResponseMessage resultadoLogin = Login(u).Result;
            string? errorMessage = null;

            if (resultadoLogin.StatusCode == HttpStatusCode.OK)
            {
                Usuario usuario = new Usuario();
                usuario.Email = u.Email;
                string token = getToken(resultadoLogin.Content);
                usuario.Token = token;
                //Se guarda el token y el usuario en los settings locales
                Properties.Settings.Default.Token = token;
                Properties.Settings.Default.Usuario = usuario.Email;
                Properties.Settings.Default.Save();

                //Se comprueba si ya ha respondido a la pregunta
                DateTime today = DateTime.Now;
                today = DateTime.SpecifyKind(today, DateTimeKind.Local);
                DateTime fecha = TimeZoneInfo.ConvertTimeToUtc(today).Date;
                Tracking hasAnswered = GetTracking(u.Email, fecha).Result;

                if (hasAnswered == null)
                {
                    //Cambio de ventana a la de respuesta
                    var currentWindow = Application.Current.MainWindow;
                    var window = new PreguntaView();
                    Application.Current.MainWindow = window;
                    window.Show();
                    currentWindow.Close();
                }
                else
                {
                    var currentWindowLogin = Application.Current.MainWindow;
                    var windowAnswered = new YaRespondido();
                    Application.Current.MainWindow = windowAnswered;
                    windowAnswered.Show();
                    currentWindowLogin.Close();
                }
            }
            else if (resultadoLogin.StatusCode == HttpStatusCode.BadRequest)
            {
                string content = resultadoLogin.Content.ReadAsStringAsync().Result;
                if (content.Contains("usuario"))
                {
                    errorMessage = "Verifica los permisos de acceso de tu cuenta";
                }
                else
                {
                    errorMessage = "Email o contraseña incorrectos";
                }
            }
            else
            {
                errorMessage = "Se ha producido un error";
            }
            return errorMessage;
        }
        public bool IsEmailValid(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string getToken(HttpContent httpContent)
        {
            string token = null;
            string content = httpContent.ReadAsStringAsync().Result;
            LoginResponse loginResp = JsonConvert.DeserializeObject<LoginResponse>(content);
            token = loginResp.AccessToken;

            return token;
        }
        public static Task<HttpResponseMessage> Login(UsuarioLogin u)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiUrl = Urls.baseURI + Urls.login;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(900);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync<UsuarioLogin>(apiUrl, u);
                response.Wait();
                return response;
            }
        }

        public async Task<Tracking> GetTracking(string email, DateTime fecha)
        {
            string fechaStr = fecha.ToString("yyyy-MM-dd");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiUrl = Urls.baseURI + Urls.getTracking + "/usuarios" + email + "/" + fechaStr;
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

            if (resp.StatusCode.Equals(HttpStatusCode.OK))
            {
                string content = resp.Content.ReadAsStringAsync().Result;
                Tracking t = JsonConvert.DeserializeObject<Tracking>(content);
                return t;
            }
            else
            {
                return null;
            }
        }
    }
}
