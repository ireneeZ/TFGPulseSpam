using PulseSpamApi.Models;

namespace PulseSpamApi.Interfaces
{
    public interface ITrackingService
    {
        public Task<List<Tracking>> GetAsync();

        public Task<Tracking?> GetAsync(string id);

        public Task<Tracking?> GetAsyncUserDate(string usuarioId, DateTime fecha);

        public Task CreateAsync(Tracking nuevoUsuario);

        public Task UpdateAsync(string id, Tracking trackingUpd);

        public Task RemoveAsync(string id);
    }
}
