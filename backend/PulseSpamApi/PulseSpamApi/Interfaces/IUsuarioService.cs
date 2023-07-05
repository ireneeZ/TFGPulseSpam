using IdentityMongo.Models;
using PulseSpamApi.Models;

namespace PulseSpamApi.Interfaces
{
    public interface IUsuarioService
    {
        public Task<List<ApplicationUser>> GetAsync();

        public Task<ApplicationUser?> GetAsync(string email);

        public Task<ApplicationUser?> GetAsyncById(string id);

        public Task UpdateAsync(string email, ApplicationUser usuarioUpd);

        public Task RemoveAsync(string email);

        public Task RemoveAsyncById(string id);
    }
}
