using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseSpamApi.Data_Transfer_Objects;
using PulseSpamApi.Interfaces;
using PulseSpamApi.Misc;
using PulseSpamApi.Models;
using PulseSpamApi.Services;
using System.Collections.Generic;
using System.Net;

namespace PulseSpamApi.Controllers
{
    [ApiController]
    [Route("api/schedules")]
    [Authorize]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IRespuestaService _respuestaService;
        private readonly IPreguntaService _preguntaService;

        public ScheduleController(IScheduleService scheduleService, IRespuestaService respuestaService,
            IPreguntaService preguntaService)
        {
            _scheduleService = scheduleService;
            _preguntaService = preguntaService;
            _respuestaService = respuestaService;
        }

        [HttpGet]
        public async Task<List<Schedule>> Get() =>
            await _scheduleService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Schedule>> Get(string id)
        {
            var schedule = await _scheduleService.GetAsync(id);

            if (schedule is null)
            {
                return NotFound();
            }

            return schedule;
        }

        [HttpGet("{fecha}")]
        public async Task<ActionResult<ScheduleTransfer>> Get(DateTime fecha)
        {
            Schedule schedule = await _scheduleService.GetAsyncDate(fecha);

            if (schedule == null)
            {
                return NotFound();
            }

            Pregunta p = await _preguntaService.GetAsync(schedule.PreguntaId);

            List<Respuesta> respuestasSchedule = new List<Respuesta>();

            if (schedule.RespuestasId != null)
            {
                foreach (var respuestaId in schedule.RespuestasId)
                {
                    List<Respuesta> respuestas = await _respuestaService.GetByQuestion(schedule.PreguntaId);

                    foreach (var respuesta in respuestas)
                    {
                        if (DateComparator.dateOnlyCompare(respuesta.Fecha, fecha))
                        {
                            respuestasSchedule.Add(respuesta);
                        }
                    }
                }
            }
            return new ScheduleTransfer(schedule, p, respuestasSchedule);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Schedule nuevoSchedule)
        {
            await _scheduleService.CreateAsync(nuevoSchedule);

            return CreatedAtAction(nameof(Get), new { id = nuevoSchedule.Id }, nuevoSchedule);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Schedule updSchedule)
        {
            var schedule = await _scheduleService.GetAsync(id);

            if (schedule is null)
            {
                return NotFound();
            }

            updSchedule.Id = schedule.Id;

            await _scheduleService.UpdateAsync(id, updSchedule);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var schedule = await _scheduleService.GetAsync(id);

            if (schedule is null)
            {
                return NotFound();
            }

            await _scheduleService.RemoveAsync(id);

            return NoContent();
        }
    }
}
