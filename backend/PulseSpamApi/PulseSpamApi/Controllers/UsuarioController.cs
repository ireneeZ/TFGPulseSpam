using IdentityMongo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseSpamApi.Interfaces;
using PulseSpamApi.Models;
using PulseSpamApi.Services;

namespace PulseSpamApi.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService) =>
            _usuarioService = usuarioService;

        [HttpGet]
        public async Task<List<ApplicationUser>> Get() =>
            await _usuarioService.GetAsync();

        [HttpGet("{email}")]
        public async Task<ActionResult<ApplicationUser>> Get(string email)
        {
            var usuario = await _usuarioService.GetAsync(email);

            if (usuario is null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<ApplicationUser>> GetById(string id)
        {
            var usuario = await _usuarioService.GetAsyncById(id);

            if (usuario is null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Update(string email, ApplicationUser updUsuario)
        {
            var usuario = await _usuarioService.GetAsync(email);

            if (usuario is null)
            {
                return NotFound();
            }

            updUsuario.Id = usuario.Id;

            await _usuarioService.UpdateAsync(email, updUsuario);

            return NoContent();
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            var usuario = await _usuarioService.GetAsync(email);

            if (usuario is null)
            {
                return NotFound();
            }

            await _usuarioService.RemoveAsync(email);

            return NoContent();
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            var usuario = await _usuarioService.GetAsyncById(id);

            if (usuario is null)
            {
                return NotFound();
            }

            await _usuarioService.RemoveAsyncById(id);

            return NoContent();
        }
    }
}
