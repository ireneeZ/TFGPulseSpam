using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseSpamApi.Interfaces;
using PulseSpamApi.Misc;
using PulseSpamApi.Models;
using PulseSpamApi.Services;

namespace PulseSpamApi.Controllers
{
    [ApiController]
    [Route("api/respuestas")]
    [Authorize]
    public class RespuestaController : ControllerBase
    {
        private readonly IRespuestaService _respuestaService;
        private readonly ITrackingService _trackingService;

        public RespuestaController(IRespuestaService respuestaService, ITrackingService trackingService)
        {
            _respuestaService = respuestaService;
            _trackingService = trackingService;
        }

        [HttpGet]
        public async Task<List<Respuesta>> Get() =>
            await _respuestaService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Respuesta>> Get(string id)
        {
            var respuesta = await _respuestaService.GetAsync(id);

            if (respuesta is null)
            {
                return NotFound();
            }

            return respuesta;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Respuesta nuevaRespuesta)
        {
            Tracking t = await _trackingService.GetAsyncUserDate(nuevaRespuesta.UsuarioId, nuevaRespuesta.Fecha);

            if (t != null && DateComparator.dateOnlyCompare(t.Fecha, nuevaRespuesta.Fecha))
            {
                return BadRequest();
            }

            await _respuestaService.CreateAsync(nuevaRespuesta);

            return CreatedAtAction(nameof(Get), new { id = nuevaRespuesta.Id }, nuevaRespuesta);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Respuesta updRespuesta)
        {
            var respuesta = await _respuestaService.GetAsync(id);

            if (respuesta is null)
            {
                return NotFound();
            }

            updRespuesta.Id = respuesta.Id;

            await _respuestaService.UpdateAsync(id, updRespuesta);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var respuesta = await _respuestaService.GetAsync(id);

            if (respuesta is null)
            {
                return NotFound();
            }

            await _respuestaService.RemoveAsync(id);

            return NoContent();
        }
    }
}
