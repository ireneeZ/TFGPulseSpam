using Microsoft.AspNetCore.Identity;
using PulseSpamApi.Data_Transfer_Objects;
using PulseSpamApi.Models;

namespace PulseSpamApi.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(UsuarioLogin usuarioLogin);

        public Task<RegistroResponse> RegistraAsync(UsuarioRegistro usuarioRegistro);

        public Task<IdentityResult> CreaRolAsync(RoleRequest roleRequest);
    }
}
