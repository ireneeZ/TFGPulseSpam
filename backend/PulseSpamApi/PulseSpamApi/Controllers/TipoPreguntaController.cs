using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseSpamApi.Interfaces;
using PulseSpamApi.Models;
using PulseSpamApi.Services;

namespace PulseSpamApi.Controllers
{
    [ApiController]
    [Route("api/tipos")]
    [Authorize]
    public class TipoPreguntaController : ControllerBase
    {
        private readonly ITipoPreguntaService _tipoPreguntaService;

        public TipoPreguntaController(ITipoPreguntaService tipoPreguntaService)
        {
            _tipoPreguntaService = tipoPreguntaService;
        }

        [HttpGet]
        public async Task<List<TipoPregunta>> Get() =>
            await _tipoPreguntaService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<TipoPregunta>> Get(string id)
        {
            var tipoPregunta = await _tipoPreguntaService.GetAsync(id);

            if (tipoPregunta is null)
            {
                return NotFound();
            }

            return tipoPregunta;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TipoPregunta nuevoTipoPregunta)
        {
            await _tipoPreguntaService.CreateAsync(nuevoTipoPregunta);

            return CreatedAtAction(nameof(Get), new { id = nuevoTipoPregunta.Id }, nuevoTipoPregunta);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, TipoPregunta updTipoPregunta)
        {
            var tipoPregunta = await _tipoPreguntaService.GetAsync(id);

            if (tipoPregunta is null)
            {
                return NotFound();
            }

            updTipoPregunta.Id = tipoPregunta.Id;

            await _tipoPreguntaService.UpdateAsync(id, updTipoPregunta);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var tipoPregunta = await _tipoPreguntaService.GetAsync(id);

            if (tipoPregunta is null)
            {
                return NotFound();
            }

            await _tipoPreguntaService.RemoveAsync(id);

            return NoContent();
        }
    }
}
