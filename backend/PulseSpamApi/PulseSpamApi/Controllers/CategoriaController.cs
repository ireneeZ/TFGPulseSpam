using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseSpamApi.Interfaces;
using PulseSpamApi.Models;
using PulseSpamApi.Services;
using System.Collections.Generic;

namespace PulseSpamApi.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IPreguntaService _preguntaService;

        public CategoriaController(ICategoriaService categoriaService, IPreguntaService preguntaService) {
            _categoriaService = categoriaService;
            _preguntaService = preguntaService;
        }

        [HttpGet]
        public async Task<List<Categoria>> Get() =>
            await _categoriaService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Categoria>> Get(string id)
        {
            var categoria = await _categoriaService.GetAsync(id);

            if (categoria is null)
            {
                return NotFound();
            }

            return categoria;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Categoria nuevaCategoria)
        {
            await _categoriaService.CreateAsync(nuevaCategoria);

            return CreatedAtAction(nameof(Get), new { id = nuevaCategoria.Id }, nuevaCategoria);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Categoria updCategoria)
        {
            var categoria = await _categoriaService.GetAsync(id);

            if (categoria is null)
            {
                return NotFound();
            }

            updCategoria.Id = categoria.Id;

            await _categoriaService.UpdateAsync(id, updCategoria);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var categoria = await _categoriaService.GetAsync(id);

            if (categoria is null)
            {
                return NotFound();
            }

            await _categoriaService.RemoveAsync(id);

            //Elimina la categoria eliminada de las preguntas que la contienen
            List<Pregunta> preguntasCategoria = await _preguntaService.GetByCategory(id);
            foreach (Pregunta pregunta in preguntasCategoria)
            {
                pregunta.CategoriaId = "6477b30b22a467cecb9144e4"; //Categoría "Ninguna"
                await _preguntaService.UpdateAsync(pregunta.Id, pregunta);
            }

            return NoContent();
        }
    }
}
