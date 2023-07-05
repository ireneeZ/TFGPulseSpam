using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Newtonsoft.Json;
using PulseSpamApi.Controllers;
using PulseSpamApi.Data_Transfer_Objects;
using PulseSpamApi.Interfaces;
using PulseSpamApi.Models;
using PulseSpamApi.Services;
using System.Diagnostics;
using Xunit.Abstractions;

namespace PulseSpamApiTests.Controllers
{
    public class TareaProgramacionControllerTest
    {
        //Para mostrar datos por consola y facilitar la depuración
        private readonly ITestOutputHelper output;

        public TareaProgramacionControllerTest(ITestOutputHelper output)
        {
            this.output = output;
            sut = new TareaProgramacionController(mockTareaService.Object,
    mockPreguntaService.Object, mockScheduleService.Object);
        }

        private TareaProgramacionController sut;

        private Mock<ITareaProgramacionService> mockTareaService = new Mock<ITareaProgramacionService>();
        private Mock<IPreguntaService> mockPreguntaService = new Mock<IPreguntaService>();
        private Mock<IScheduleService> mockScheduleService = new Mock<IScheduleService>();

        /************/
        /****GET****/
        /************/

        [Fact]
        public void GetTest()
        {
            //Arrange
            Pregunta pregunta1 = new Pregunta();
            pregunta1.Id = ObjectId.GenerateNewId().ToString();
            pregunta1.PreguntaTxt = "Pregunta1";

            Pregunta pregunta2 = new Pregunta();
            pregunta2.Id = ObjectId.GenerateNewId().ToString();
            pregunta2.PreguntaTxt = "Pregunta2";

            TareaProgramacion tarea = new TareaProgramacion();
            tarea.Id = "T001";
            tarea.FechaInicio = DateTime.Now;
            tarea.FechaFin = DateTime.Now.AddDays(1);
            tarea.PreguntasId = new string[] { pregunta1.Id, pregunta2.Id };

            List<Pregunta> preguntas = new List<Pregunta>
            {
                pregunta1,
                pregunta2
            };
            TareaProgramacionTransfer tareaTransfer = new TareaProgramacionTransfer(tarea, preguntas);
            List<TareaProgramacionTransfer> listTareaTransfer = new List<TareaProgramacionTransfer>();
            listTareaTransfer.Add(tareaTransfer);

            List<TareaProgramacion> programacionList = new List<TareaProgramacion>();
            programacionList.Add(tarea);

            mockTareaService.Setup(x => x.GetAsync()).Returns(Task.FromResult(programacionList));
            mockPreguntaService.Setup(x => x.GetAsync(pregunta1.Id)).Returns(Task.FromResult(pregunta1));
            mockPreguntaService.Setup(x => x.GetAsync(pregunta2.Id)).Returns(Task.FromResult(pregunta2));

            //Act
            List<TareaProgramacionTransfer> programacion = sut.Get().Result;

            //Assert
            var object1Json = JsonConvert.SerializeObject(listTareaTransfer);
            var object2Json = JsonConvert.SerializeObject(programacion);

            output.WriteLine("Esperado: " + object1Json);
            output.WriteLine("Actual: " + object2Json);

            Assert.Equal(object1Json, object2Json);
        }

        /***************/
        /****GET(id)****/
        /***************/

        [Fact]
        public void GetIdTest()
        {
            //Arrange
            Pregunta pregunta1 = new Pregunta();
            pregunta1.Id = ObjectId.GenerateNewId().ToString();
            pregunta1.PreguntaTxt = "Pregunta1";

            Pregunta pregunta2 = new Pregunta();
            pregunta2.Id = ObjectId.GenerateNewId().ToString();
            pregunta2.PreguntaTxt = "Pregunta2";

            TareaProgramacion tarea = new TareaProgramacion();
            tarea.Id = "T001";
            tarea.FechaInicio = DateTime.Now;
            tarea.FechaFin = DateTime.Now.AddDays(1);
            tarea.PreguntasId = new string[] { pregunta1.Id, pregunta2.Id };

            List<Pregunta> preguntas = new List<Pregunta>
            {
                pregunta1,
                pregunta2
            };
            TareaProgramacionTransfer tareaTransfer = new TareaProgramacionTransfer(tarea, preguntas);

            mockTareaService.Setup(x => x.GetAsync(tarea.Id)).Returns(Task.FromResult(tarea));
            mockPreguntaService.Setup(x => x.GetAsync(pregunta1.Id)).Returns(Task.FromResult(pregunta1));
            mockPreguntaService.Setup(x => x.GetAsync(pregunta2.Id)).Returns(Task.FromResult(pregunta2));

            //Act
            ActionResult<TareaProgramacionTransfer> programacionAction = sut.Get(tarea.Id).Result;
            TareaProgramacionTransfer programacion = programacionAction.Value;

            //Assert
            var object1Json = JsonConvert.SerializeObject(programacion);
            var object2Json = JsonConvert.SerializeObject(tareaTransfer);

            output.WriteLine("Esperado: " + object1Json);
            output.WriteLine("Actual: " + object2Json);

            Assert.Equal(object1Json, object2Json);
        }

        [Fact]
        public void GetIdNoValidoTest()
        {
            //Arrange
            Pregunta pregunta1 = new Pregunta();
            pregunta1.Id = ObjectId.GenerateNewId().ToString();
            pregunta1.PreguntaTxt = "Pregunta1";

            Pregunta pregunta2 = new Pregunta();
            pregunta2.Id = ObjectId.GenerateNewId().ToString();
            pregunta2.PreguntaTxt = "Pregunta2";

            TareaProgramacion tarea = new TareaProgramacion();
            tarea.Id = "T001";
            tarea.FechaInicio = DateTime.Now;
            tarea.FechaFin = DateTime.Now.AddDays(1);
            tarea.PreguntasId = new string[] { pregunta1.Id, pregunta2.Id };

            List<Pregunta> preguntas = new List<Pregunta>
            {
                pregunta1,
                pregunta2
            };
            TareaProgramacionTransfer tareaTransfer = new TareaProgramacionTransfer(tarea, preguntas);

            mockTareaService.Setup(x => x.GetAsync(tarea.Id)).Returns(Task.FromResult(tarea));
            mockPreguntaService.Setup(x => x.GetAsync(pregunta1.Id)).Returns(Task.FromResult(pregunta1));
            mockPreguntaService.Setup(x => x.GetAsync(pregunta2.Id)).Returns(Task.FromResult(pregunta2));

            //Act
            ActionResult<TareaProgramacionTransfer> programacionAction = sut.Get("T1234").Result;

            var obj = JsonConvert.SerializeObject(programacionAction);
            output.WriteLine("Retornado: " + obj);

            //Assert
            Assert.IsType<NotFoundResult>(programacionAction.Result);
        }

        /***************/
        /******POST*****/
        /***************/

        [Fact]
        public void PostSimpleTest()
        {
            //Arrange
            Pregunta pregunta1 = new Pregunta();
            pregunta1.Id = ObjectId.GenerateNewId().ToString();
            pregunta1.PreguntaTxt = "Pregunta1";

            Pregunta pregunta2 = new Pregunta();
            pregunta2.Id = ObjectId.GenerateNewId().ToString();
            pregunta2.PreguntaTxt = "Pregunta2";

            TareaProgramacion tarea = new TareaProgramacion();
            tarea.Id = "T001";
            tarea.FechaFin = DateTime.Now.AddDays(1);
            tarea.FechaInicio = DateTime.Now;
            tarea.PreguntasId = new string[] { pregunta1.Id, pregunta2.Id };

            List<Pregunta> preguntas = new List<Pregunta>
            {
                pregunta1,
                pregunta2
            };

            mockTareaService.Setup(x => x.GetAsync(tarea.Id)).Returns(Task.FromResult(tarea));
            mockPreguntaService.Setup(x => x.GetAsync(pregunta1.Id)).Returns(Task.FromResult(pregunta1));
            mockPreguntaService.Setup(x => x.GetAsync(pregunta2.Id)).Returns(Task.FromResult(pregunta2));

            //Act
            IActionResult programacionAction = sut.Post(tarea).Result;

            var obj = JsonConvert.SerializeObject(programacionAction);
            output.WriteLine("Retornado: " + obj);

            //Assert
            Assert.IsType<CreatedAtActionResult>(programacionAction);
        }

        [Fact]
        public void PostFechaFinMayorTest()
        {
            //Arrange
            Pregunta pregunta1 = new Pregunta();
            pregunta1.Id = ObjectId.GenerateNewId().ToString();
            pregunta1.PreguntaTxt = "Pregunta1";

            Pregunta pregunta2 = new Pregunta();
            pregunta2.Id = ObjectId.GenerateNewId().ToString();
            pregunta2.PreguntaTxt = "Pregunta2";

            TareaProgramacion tarea = new TareaProgramacion();
            tarea.Id = "T001";
            tarea.FechaFin = DateTime.Now;
            tarea.FechaInicio = DateTime.Now.AddDays(1);
            tarea.PreguntasId = new string[] { pregunta1.Id, pregunta2.Id };

            List<Pregunta> preguntas = new List<Pregunta>
            {
                pregunta1,
                pregunta2
            };

            //Act
            IActionResult programacionAction = sut.Post(tarea).Result;


            var obj = JsonConvert.SerializeObject(programacionAction);
            output.WriteLine("Retornado: " + obj);

            //Assert
            Assert.IsType<NotFoundObjectResult>(programacionAction);
            Assert.Contains("FechaIncorrecta", obj);
        }

        [Fact]
        public void PostIgualFechasTest()
        {
            //Arrange
            Pregunta pregunta1 = new Pregunta();
            pregunta1.Id = ObjectId.GenerateNewId().ToString();
            pregunta1.PreguntaTxt = "Pregunta1";

            Pregunta pregunta2 = new Pregunta();
            pregunta2.Id = ObjectId.GenerateNewId().ToString();
            pregunta2.PreguntaTxt = "Pregunta2";

            TareaProgramacion tarea = new TareaProgramacion();
            tarea.Id = "T001";
            tarea.FechaFin = DateTime.Now;
            tarea.FechaInicio = tarea.FechaFin;
            tarea.PreguntasId = new string[] { pregunta1.Id, pregunta2.Id };

            List<Pregunta> preguntas = new List<Pregunta>
            {
                pregunta1,
                pregunta2
            };

            //Act
            IActionResult programacionAction = sut.Post(tarea).Result;


            var obj = JsonConvert.SerializeObject(programacionAction);
            output.WriteLine("Retornado: " + obj);

            //Assert
            Assert.IsType<NotFoundObjectResult>(programacionAction);
            Assert.Contains("FechaIncorrecta", obj);
        }

        [Fact]
        public void PostMasPreguntasQueDiasTest()
        {
            //Arrange
            Pregunta pregunta1 = new Pregunta();
            pregunta1.Id = ObjectId.GenerateNewId().ToString();
            pregunta1.PreguntaTxt = "Pregunta1";

            Pregunta pregunta2 = new Pregunta();
            pregunta2.Id = ObjectId.GenerateNewId().ToString();
            pregunta2.PreguntaTxt = "Pregunta2";

            Pregunta pregunta3 = new Pregunta();
            pregunta3.Id = ObjectId.GenerateNewId().ToString();
            pregunta3.PreguntaTxt = "Pregunta3";

            TareaProgramacion tarea = new TareaProgramacion();
            tarea.Id = "T001";
            tarea.FechaInicio = DateTime.Now;
            tarea.FechaFin = DateTime.Now.AddDays(1);
            tarea.PreguntasId = new string[] { pregunta1.Id, pregunta2.Id, pregunta3.Id };

            List<Pregunta> preguntas = new List<Pregunta>
            {
                pregunta1,
                pregunta2,
                pregunta3
            };

            //Act
            IActionResult programacionAction = sut.Post(tarea).Result;

            var obj = JsonConvert.SerializeObject(programacionAction);
            output.WriteLine("Retornado: " + obj);

            //Assert
            Assert.IsType<NotFoundObjectResult>(programacionAction);
            Assert.Contains("MasPreguntasQueDias", obj);
        }
    }
}