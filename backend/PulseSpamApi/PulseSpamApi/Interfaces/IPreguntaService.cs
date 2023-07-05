using PulseSpamApi.Models;

namespace PulseSpamApi.Interfaces
{
    public interface IPreguntaService
    {
        public Task<List<Pregunta>> GetAsync();

        public Task<Pregunta?> GetAsync(string id);

        public Task<List<Pregunta>> GetByCategory(string categoria);

        public Task CreateAsync(Pregunta nuevaPregunta);

        public Task UpdateAsync(string id, Pregunta preguntaUpd);

        public Task RemoveAsync(string id);
    }
}
