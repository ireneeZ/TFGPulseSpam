using PulseSpamApi.Models;

namespace PulseSpamApi.Interfaces
{
    public interface ICategoriaService
    {
        public Task<List<Categoria>> GetAsync();

        public Task<Categoria?> GetAsync(string id);

        public Task CreateAsync(Categoria nuevaCategoria);

        public Task UpdateAsync(string id, Categoria categoriaUpd);

        public Task RemoveAsync(string id);
    }
}
