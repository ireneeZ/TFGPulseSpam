using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseSpamApi.Data_Transfer_Objects;
using PulseSpamApi.Interfaces;
using PulseSpamApi.Models;
using PulseSpamApi.Misc;
using PulseSpamApi.Services;
using System.Collections.Generic;
using System.Net;

namespace PulseSpamApi.Controllers
{
    [ApiController]
    [Route("api/tareas")]
    [Authorize]
    public class TareaProgramacionController : ControllerBase
    {
        private readonly ITareaProgramacionService _tareaService;
        private readonly IPreguntaService _preguntaService;
        private readonly IScheduleService _scheduleService;

        public TareaProgramacionController(ITareaProgramacionService tareaService, 
            IPreguntaService preguntaService, IScheduleService scheduleService)
        {
            _tareaService = tareaService;
            _preguntaService = preguntaService; 
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<List<TareaProgramacionTransfer>> Get()
        {
            List<TareaProgramacion> tareas = await _tareaService.GetAsync();
            List<TareaProgramacionTransfer> tareasTransfer = new List<TareaProgramacionTransfer>();

            foreach (TareaProgramacion tarea in tareas)
            {
                string[] tareaPreguntasIds = tarea.PreguntasId;
                List<Pregunta> preguntasTarea = new List<Pregunta>();

                foreach (string idPregunta in tareaPreguntasIds)
                {
                    Pregunta p = await _preguntaService.GetAsync(idPregunta);
                    preguntasTarea.Add(p);
                }
                tareasTransfer.Add(new TareaProgramacionTransfer(tarea, preguntasTarea));
            }
            return tareasTransfer;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<TareaProgramacionTransfer>> Get(string id)
        {
            var tarea = await _tareaService.GetAsync(id);

            if (tarea is null)
            {
                return NotFound();
            }

            string[] tareaPreguntasIds = tarea.PreguntasId;
            List<Pregunta> preguntasTarea = new List<Pregunta>();
            foreach (string idPregunta in tareaPreguntasIds)
            {
                Pregunta? p = await _preguntaService.GetAsync(idPregunta);

                if (p is null)
                {
                    return NotFound();
                }

                preguntasTarea.Add(p);
            }
            TareaProgramacionTransfer tareaTransfer = 
                new TareaProgramacionTransfer(tarea, preguntasTarea);

            return tareaTransfer;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TareaProgramacion nuevaTareaProgramacion)
        {
            DateTime.SpecifyKind(nuevaTareaProgramacion.FechaInicio, DateTimeKind.Utc);
            DateTime.SpecifyKind(nuevaTareaProgramacion.FechaFin, DateTimeKind.Utc);

            DateTime fechaIni = TimeZoneInfo.ConvertTime(nuevaTareaProgramacion.FechaInicio, TimeZoneInfo.Local);
            DateTime fechaFin = TimeZoneInfo.ConvertTime(nuevaTareaProgramacion.FechaFin, TimeZoneInfo.Local);

            int dias = (fechaFin.Date - fechaIni.Date).Days;
            dias++;

            //Gestion de posibles errores de validacion
            if (dias <= 0 || nuevaTareaProgramacion.FechaInicio == nuevaTareaProgramacion.FechaFin
                || nuevaTareaProgramacion.FechaInicio > nuevaTareaProgramacion.FechaFin)
            {
                ModelState.AddModelError("FechaIncorrecta", "La fecha elegida no es correcta.");
                return NotFound(ModelState);
            } else if (nuevaTareaProgramacion.PreguntasId.Length > dias)
            {
                ModelState.AddModelError("MasPreguntasQueDias", "El numero de preguntas propuesto debe ser igual o menor que el número de dias en la fecha.");
                return NotFound(ModelState);
            }

            await _tareaService.CreateAsync(nuevaTareaProgramacion);

            while (!DateComparator.dateOnlyCompare(fechaIni, fechaFin))
            {
                foreach (string idPregunta in nuevaTareaProgramacion.PreguntasId)
                {
                    Schedule s = new Schedule();
                    s.PreguntaId = idPregunta;
                    s.FechaPregunta = fechaIni; 
                    await _scheduleService.CreateAsync(s);
 
                    if (DateComparator.dateOnlyCompare(fechaIni, fechaFin))
                    {
                        goto Fin;
                    } else
                    {
                        fechaIni = fechaIni.AddDays(1);
                    }
                }
            }

            Fin:
            return CreatedAtAction(nameof(Get), new { id = nuevaTareaProgramacion.Id }, nuevaTareaProgramacion);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var tarea = await _tareaService.GetAsync(id);

            if (tarea is null)
            {
                return NotFound();
            }

            string[] preguntasId = tarea.PreguntasId;
            List<Schedule> s = new List<Schedule>();

            foreach (string idPregunta in preguntasId)
            {
                s = await _scheduleService.GetAsyncPregunta(idPregunta);
            }

            if (s.Count == 0)
            {
                return NotFound();
            }
            else
            {
                foreach (Schedule sched in s)
                {
                    await _scheduleService.RemoveAsync(sched.Id);
                }
            }

            await _tareaService.RemoveAsync(id);

            return NoContent();
        }
    }
}
