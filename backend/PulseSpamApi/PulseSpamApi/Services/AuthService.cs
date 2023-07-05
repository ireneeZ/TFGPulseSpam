using IdentityMongo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PulseSpamApi.Data_Transfer_Objects;
using PulseSpamApi.Interfaces;
using PulseSpamApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;

namespace PulseSpamApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<LoginResponse> LoginAsync(UsuarioLogin usuarioLogin)
        {
            var usuario = await _userManager.FindByEmailAsync(usuarioLogin.Email);

            if (usuario != null)
            {
                var pass = await _userManager.CheckPasswordAsync(usuario, usuarioLogin.Password);
                if (!pass)
                {
                    return new LoginResponse { Message = "Login incorrecto", Success = false };
                }
            } else
            {
                return new LoginResponse { Message = "Login incorrecto", Success = false };
            }

            var claims = new List<Claim>
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),

            };

            var roles = await _userManager.GetRolesAsync(usuario);

            if (usuarioLogin.IsAdmin) {
                if(! await _userManager.IsInRoleAsync(usuario, "ADMIN"))
                {
                    return new LoginResponse { Message = "No es administrador", Success = false };
                }
            } else
            {
                if (!await _userManager.IsInRoleAsync(usuario, "USER"))
                {
                    return new LoginResponse { Message = "No es usuario normal", Success = false };
                }
            }

            var rolesClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
            claims.AddRange(rolesClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3g5f4g45345hbu90b2"));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var expires = DateTime.Now.AddMinutes(30);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: claims,
                //expires: expires,
                signingCredentials: credenciales
            );

            return new LoginResponse()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Message = "Login correcto",
                Email = usuario.Email,
                Success = true,
                UserId = usuario.Id.ToString()
            };
        }

        public async Task<RegistroResponse> RegistraAsync(UsuarioRegistro usuarioRegistro)
        {
            var usuario = await _userManager.FindByEmailAsync(usuarioRegistro.Email);

            if (usuario != null)
            {
                return new RegistroResponse { Message = "El usuario ya existe", Success = false };
            }

            usuario = new ApplicationUser
            {
                Email = usuarioRegistro.Email,
                UserName = usuarioRegistro.UserName,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };

            var creaUsuario = await _userManager.CreateAsync(usuario, usuarioRegistro.Password);

            if (!creaUsuario.Succeeded)
            {
                return new RegistroResponse { Message = "No se ha podido crear el usuario", Success = false };
            }

            string rolUsuario = "USER";
            if (usuarioRegistro.isAdmin)
            {
                rolUsuario = "ADMIN";
            }

            var role = await _userManager.AddToRoleAsync(usuario, rolUsuario);
            if (!role.Succeeded)
            {
                return new RegistroResponse { Message = "Error añadiendo el rol al usuario", Success = false };
            }

            return new RegistroResponse
            {
                Success = true,
                Message = "Usuario registrado correctamente"
            };
        }

        public async Task<IdentityResult> CreaRolAsync(RoleRequest roleRequest)
        {
            var rolLocal = new ApplicationRole { Name = roleRequest.Role };
            var rol = await _roleManager.CreateAsync(rolLocal);

            return rol;
        }
    }
}
