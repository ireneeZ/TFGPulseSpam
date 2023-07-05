using DnsClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PulseSpamApi.Data_Transfer_Objects;
using PulseSpamApi.Interfaces;
using PulseSpamApi.Models;
using PulseSpamApi.Services;
using System.ComponentModel.DataAnnotations;

namespace PulseSpamApi.Controllers
{
    [ApiController]
    [Route("api/auth/")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
        {
            LoginResponse login = await _authService.LoginAsync(usuarioLogin);

            if (login.Success)
            {
                return Ok(login);
            }
            else
            {
                return BadRequest(login.Message);
            }
        }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> RegistraUsuario(UsuarioRegistro usuarioRegistro)
        {
            var registro = await _authService.RegistraAsync(usuarioRegistro);

            if (registro.Success)
            {
                return Ok(registro);
            }
            else
            {
                return BadRequest(registro.Message);
            }
        }

        [HttpPost]
        [Route("roles")]
        public async Task<IActionResult> creaRol(RoleRequest roleRequest)
        {
            var rol = await _authService.CreaRolAsync(roleRequest);

            if (rol != null)
            {
                return Ok(rol);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
