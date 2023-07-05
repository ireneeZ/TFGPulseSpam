using PulseSpamApi.Models;

namespace PulseSpamApi.Interfaces
{
    public interface IDepartamentoService
    {
        public Task<List<Departamento>> GetAsync();

        public Task<Departamento?> GetAsync(string id);

        public Task CreateAsync(Departamento nuevoDepartamento);

        public Task UpdateAsync(string id, Departamento dptoUpd);

        public Task RemoveAsync(string id);
    }
}
