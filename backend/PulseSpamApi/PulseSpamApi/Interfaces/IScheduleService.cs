using PulseSpamApi.Models;

namespace PulseSpamApi.Interfaces
{
    public interface IScheduleService
    {
        public Task<List<Schedule>> GetAsync();

        public Task<Schedule?> GetAsync(string id);

        public Task<Schedule> GetAsyncDate(DateTime fecha);

        public Task<List<Schedule>> GetAsyncPregunta(string preguntaId);

        public Task CreateAsync(Schedule nuevoTracking);

        public Task UpdateAsync(string id, Schedule schedulingUpd);

        public Task RemoveAsync(string id);
    }
}
