using PulseSpamApi.Models;

namespace PulseSpamApi.Interfaces
{
    public interface ITipoPreguntaService
    {
        public Task<List<TipoPregunta>> GetAsync();

        public Task<TipoPregunta?> GetAsync(string id);

        public Task CreateAsync(TipoPregunta nuevoTipoPregunta);

        public Task UpdateAsync(string id, TipoPregunta tipoPreguntaUpd);

        public Task RemoveAsync(string id);
    }
}
