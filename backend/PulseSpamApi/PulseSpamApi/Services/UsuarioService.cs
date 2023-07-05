using Microsoft.Extensions.Options;
using PulseSpamApi.Models;
using MongoDB.Driver;
using PulseSpamApi.Interfaces;
using IdentityMongo.Models;
using Microsoft.AspNetCore.Identity;

namespace PulseSpamApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioService(
            IOptions<PulseSpamDatabaseSettings> pulseSpamDatabaseSettings, UserManager<ApplicationUser> userManager)
        {
            var mongoClient = new MongoClient(
                pulseSpamDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                pulseSpamDatabaseSettings.Value.DatabaseName);

            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetAsync() {
            List<ApplicationUser> lista = new List<ApplicationUser>();
            await Task.Run(() =>
            {
                lista = _userManager.Users.ToList();
            });
            return lista;
        }

        public async Task<ApplicationUser?> GetAsync(string email) {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser?> GetAsyncById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task UpdateAsync(string email, ApplicationUser usuarioUpd)
        {
            ApplicationUser usuarioBd = await _userManager.FindByEmailAsync(email);
            if (usuarioBd != null)
            {
                await _userManager.UpdateAsync(usuarioUpd);
            }
        }

        public async Task RemoveAsync(string email)
        {
            ApplicationUser usuarioBd = await _userManager.FindByEmailAsync(email);
            if (usuarioBd != null)
            {
                await _userManager.DeleteAsync(usuarioBd);
            }
        }

        public async Task RemoveAsyncById(string id)
        {
            ApplicationUser usuarioBd = await _userManager.FindByIdAsync(id);
            if (usuarioBd != null)
            {
                await _userManager.DeleteAsync(usuarioBd);
            }
        }
    }
}
