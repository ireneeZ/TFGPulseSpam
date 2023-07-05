using Microsoft.Extensions.Options;
using PulseSpamApi.Models;
using MongoDB.Driver;
using PulseSpamApi.Interfaces;

namespace PulseSpamApi.Services
{
    public class ScheduleService: IScheduleService
    {
        private readonly IMongoCollection<Schedule> _SchedulingCollection;

        public ScheduleService(
            IOptions<PulseSpamDatabaseSettings> pulseSpamDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                pulseSpamDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                pulseSpamDatabaseSettings.Value.DatabaseName);

            _SchedulingCollection = mongoDatabase.GetCollection<Schedule>(
                pulseSpamDatabaseSettings.Value.SchedulingCollectionName);
        }

        public async Task<List<Schedule>> GetAsync() =>
            await _SchedulingCollection.Find(_ => true).ToListAsync();

        public async Task<Schedule?> GetAsync(string id) =>
            await _SchedulingCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Schedule> GetAsyncDate(DateTime fecha) =>
            await _SchedulingCollection.Find(x => x.FechaPregunta.Year == fecha.Year && x.FechaPregunta.Month == fecha.Month && x.FechaPregunta.Day == fecha.Day).FirstOrDefaultAsync();

        public async Task<List<Schedule>> GetAsyncPregunta(string preguntaId) =>
            await _SchedulingCollection.Find(x => x.PreguntaId == preguntaId).ToListAsync();

        public async Task CreateAsync(Schedule nuevoScheduling) =>
            await _SchedulingCollection.InsertOneAsync(nuevoScheduling);

        public async Task UpdateAsync(string id, Schedule SchedulingUpd) =>
            await _SchedulingCollection.ReplaceOneAsync(x => x.Id == id, SchedulingUpd);

        public async Task RemoveAsync(string id) =>
            await _SchedulingCollection.DeleteOneAsync(x => x.Id == id);

    }
}
