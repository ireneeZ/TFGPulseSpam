using PulseSpamApi.Models;

namespace PulseSpamApi.Interfaces
{
    public interface IRespuestaService
    {
        public Task<List<Respuesta>> GetAsync();

        public Task<Respuesta?> GetAsync(string id);

        public Task CreateAsync(Respuesta nuevaRespuesta);

        public Task UpdateAsync(string id, Respuesta respuestaUpd);

        public Task RemoveAsync(string id);

        public Task<List<Respuesta>> GetByQuestion(String preguntaId);
    }
}
