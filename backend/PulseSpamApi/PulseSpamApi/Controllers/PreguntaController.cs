using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseSpamApi.Data_Transfer_Objects;
using PulseSpamApi.Interfaces;
using PulseSpamApi.Models;
using PulseSpamApi.Services;

namespace PulseSpamApi.Controllers
{
    [ApiController]
    [Route("api/preguntas")]
    [Authorize]
    public class PreguntaController : ControllerBase
    {
        private readonly IPreguntaService _preguntaService;

        private readonly ICategoriaService _categoriaService;

        private readonly ITipoPreguntaService _tipoPreguntaService;

        public PreguntaController(IPreguntaService preguntaService, ICategoriaService categoriaService, 
            ITipoPreguntaService tipoPreguntaService)
        {
            _preguntaService = preguntaService;
            _categoriaService = categoriaService;
            _tipoPreguntaService = tipoPreguntaService;
        }

        [HttpGet]
        public async Task<List<PreguntaTransfer>> Get([FromQuery(Name = "idCategoria")] string? idCategoria = null,
            [FromQuery(Name = "idTipo")] string? idTipo = null)
        {
            List<Pregunta> preguntas = await _preguntaService.GetAsync();
            List<TipoPregunta> tipos = await _tipoPreguntaService.GetAsync();
            List<Categoria> categorias = await _categoriaService.GetAsync();
            List<PreguntaTransfer> preguntasTransfer = new List<PreguntaTransfer>();

            foreach (var pregunta in preguntas)
            {
                if (pregunta.CategoriaId != null && pregunta.TipoId != null) {
                    Categoria? categoriaPregunta = categorias.Find(x => x.Id == pregunta.CategoriaId);
                    TipoPregunta? tipoPregunta = tipos.Find(x => x.Id == pregunta.TipoId);
                    PreguntaTransfer preguntaTransfer = new PreguntaTransfer(pregunta, 
                        categoriaPregunta, tipoPregunta);

                    if (idCategoria != null)
                    {
                        if (pregunta.CategoriaId.Equals(idCategoria))
                        {
                            preguntasTransfer.Add(preguntaTransfer);
                        }
                    } else if (idTipo != null) 
                    {
                        if (pregunta.TipoId.Equals(idTipo))
                        {
                            preguntasTransfer.Add(preguntaTransfer);
                        }
                    } else
                    {
                        preguntasTransfer.Add(preguntaTransfer);
                    }
            }
            }

            return preguntasTransfer;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PreguntaTransfer>> Get(string id)
        {
            var pregunta = await _preguntaService.GetAsync(id);

            if (pregunta is null)
            {
                return NotFound();
            }

            Categoria categoriaPregunta = await _categoriaService.GetAsync(pregunta.CategoriaId);
            TipoPregunta tipoPregunta = await _tipoPreguntaService.GetAsync(pregunta.TipoId);
            PreguntaTransfer preguntaTransfer = new PreguntaTransfer(pregunta, categoriaPregunta, tipoPregunta);
            preguntaTransfer.CategoriaCat = categoriaPregunta.CategoriaCat;
            preguntaTransfer.Tipo = tipoPregunta.Tipo;

            return preguntaTransfer;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pregunta nuevaPregunta)
        {
            await _preguntaService.CreateAsync(nuevaPregunta);

            return CreatedAtAction(nameof(Get), new { id = nuevaPregunta.Id }, nuevaPregunta);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Pregunta updPregunta)
        {
            var pregunta = await _preguntaService.GetAsync(id);

            if (pregunta is null)
            {
                return NotFound();
            }

            updPregunta.Id = pregunta.Id;

            await _preguntaService.UpdateAsync(id, updPregunta);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var pregunta = await _preguntaService.GetAsync(id);

            if (pregunta is null)
            {
                return NotFound();
            }

            await _preguntaService.RemoveAsync(id);

            return NoContent();
        }
    }
}
