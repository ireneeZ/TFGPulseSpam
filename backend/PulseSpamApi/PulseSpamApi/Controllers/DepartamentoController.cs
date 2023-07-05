using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseSpamApi.Interfaces;
using PulseSpamApi.Models;
using PulseSpamApi.Services;

namespace PulseSpamApi.Controllers
{
    [ApiController]
    [Route("api/departamentos")]
    [Authorize]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IDepartamentoService departamentoService) =>
            _departamentoService = departamentoService;

        [HttpGet]
        public async Task<List<Departamento>> Get() =>
            await _departamentoService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Departamento>> Get(string id)
        {
            var departamento = await _departamentoService.GetAsync(id);

            if (departamento is null)
            {
                return NotFound();
            }

            return departamento;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Departamento nuevaDepartamento)
        {
            await _departamentoService.CreateAsync(nuevaDepartamento);

            return CreatedAtAction(nameof(Get), new { id = nuevaDepartamento.Id }, nuevaDepartamento);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Departamento updDepartamento)
        {
            var departamento = await _departamentoService.GetAsync(id);

            if (departamento is null)
            {
                return NotFound();
            }

            updDepartamento.Id = departamento.Id;

            await _departamentoService.UpdateAsync(id, updDepartamento);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var departamento = await _departamentoService.GetAsync(id);

            if (departamento is null)
            {
                return NotFound();
            }

            await _departamentoService.RemoveAsync(id);

            return NoContent();
        }
    }
}
