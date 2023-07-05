using PulseSpamDesktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using PulseSpamDesktop.View;
using System.Windows;
using PulseSpamDesktop.Converters;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text.Json.Nodes;
using System.IO;
using PulseSpamDesktop.Service;

namespace PulseSpamDesktop.ViewModel
{
    public class PreguntaVM : BaseVM
    {
        private Pregunta _preguntaObject;
        private ScheduleTransfer _scheduleObject;
        private string _pregunta;
        private DateTime currentDate = DateTime.Now;

        private bool _isRespuestaLibreVisible = false;
        private bool _isRespuestaSiNoVisible = false;
        private bool _isRespuestaPuntuacionVisible = false;

        private string _respuestaLibre;
        private bool _respuestaSiNo;
        private string _respuestaPuntuacion;
        private string[] _puntuaciones = new string[] { "1", "2", "3", "4", "5" };

        private PreguntaService _preguntaService;

        public string Pregunta
        {
            get
            {
                return _pregunta;
            }

            set
            {
                _pregunta = value;
                OnPropertyChanged(nameof(_pregunta));
            }
        }

        public bool IsRespuestaLibreVisible
        {
            get
            {
                return _isRespuestaLibreVisible;
            }

            set
            {
                _isRespuestaLibreVisible = value;
                OnPropertyChanged(nameof(_isRespuestaLibreVisible));
            }
        }

        public bool IsRespuestaSiNoVisible
        {
            get
            {
                return _isRespuestaSiNoVisible;
            }

            set
            {
                _isRespuestaSiNoVisible = value;
                OnPropertyChanged(nameof(_isRespuestaSiNoVisible));
            }
        }

        public bool IsRespuestaPuntuacionVisible
        {
            get
            {
                return _isRespuestaPuntuacionVisible;
            }

            set
            {
                _isRespuestaPuntuacionVisible = value;
                OnPropertyChanged(nameof(_isRespuestaPuntuacionVisible));
            }
        }

        public string RespuestaLibre
        {
            get
            {
                return _respuestaLibre;
            }

            set
            {
                _respuestaLibre = value;
                OnPropertyChanged(nameof(_respuestaLibre));
            }
        }

        public bool RespuestaSiNo
        {
            get
            {
                return _respuestaSiNo;
            }

            set
            {
                _respuestaSiNo = value;
                OnPropertyChanged(nameof(_respuestaSiNo));
            }
        }

        public string RespuestaPuntuacion
        {
            get
            {
                return _respuestaPuntuacion;
            }

            set
            {
                _respuestaPuntuacion = value;
                OnPropertyChanged(nameof(_respuestaPuntuacion));
            }
        }

        public string[] Puntuaciones
        {
            get
            {
                return _puntuaciones;
            }

            set
            {
                _puntuaciones = value;
                OnPropertyChanged(nameof(_puntuaciones));
            }
        }

        public ICommand ResponderCommand { get; }
        public ICommand OmitirCommand { get; }

        public PreguntaVM()
        {
            _preguntaService = new PreguntaService();

            currentDate = DateTime.SpecifyKind(currentDate, DateTimeKind.Local);

            ResponderCommand = new DelegateCommand(ExecuteResponderCommand);
            OmitirCommand = new DelegateCommand(ExecuteOmitirCommand);

            //Se carga la pregunta según el schedule asociado a la fecha actual
            ScheduleTransfer s = _preguntaService.GetSchedule(DateFormatConverter.dateFormatConverter(currentDate));
            _preguntaObject = _preguntaService.GetPregunta(s.Pregunta.Id);
            _scheduleObject = s;
            _pregunta = s.Pregunta.PreguntaTxt;

            if (_preguntaObject.Tipo.Equals("Respuesta libre"))
            {
                _isRespuestaLibreVisible = true;
            }
            else if (_preguntaObject.Tipo.Equals("Puntuación (1-5)"))
            {
                _isRespuestaPuntuacionVisible = true;
            }
            else if (_preguntaObject.Tipo.Equals("Si/No"))
            {
                _isRespuestaSiNoVisible = true;
            }
        }

        private void ExecuteResponderCommand()
        {
            Respuesta r = new Respuesta();
            r.Fecha = currentDate;
            r.PreguntaId = _preguntaObject.Id;
            r.UsuarioId = Properties.Settings.Default.Usuario;

            if (_preguntaObject.Tipo.Equals("Respuesta libre"))
            {
                r.Tipo = "Libre";
                r.RespuestaLibre = RespuestaLibre;
            }
            else if (_preguntaObject.Tipo.Equals("Si/No"))
            {
                r.Tipo = "SiNo";
                r.RespuestaSiNo = RespuestaSiNo;
            }
            else if (_preguntaObject.Tipo.Equals("Puntuación (1-5)"))
            {
                r.Tipo = "Puntuacion";
                r.RespuestaPuntuacion = Int32.Parse(RespuestaPuntuacion);
            }

            _preguntaService.ExecuteResponderCommand(r, _scheduleObject);
        }

        private void ExecuteOmitirCommand(){
            Tracking t = new Tracking();
            t.UsuarioId = Properties.Settings.Default.Usuario;
            t.Fecha = currentDate;
            _preguntaService.ExecuteOmitirCommand(t);
        }
    }
}
