using PulseSpamApi.Models;

namespace PulseSpamApi.Interfaces
{
    public interface ITareaProgramacionService
    {
        public Task<List<TareaProgramacion>> GetAsync();

        public Task<TareaProgramacion?> GetAsync(string id);

        public Task CreateAsync(TareaProgramacion nuevaTarea);

        public Task UpdateAsync(string id, TareaProgramacion tareaUpd);

        public Task RemoveAsync(string id);
    }
}
