using Microsoft.Extensions.Options;
using PulseSpamApi.Models;
using MongoDB.Driver;
using PulseSpamApi.Interfaces;

namespace PulseSpamApi.Services
{
    public class TareaProgramacionService: ITareaProgramacionService
    {
        private readonly IMongoCollection<TareaProgramacion> _TareaProgramacionCollection;

        public TareaProgramacionService(
            IOptions<PulseSpamDatabaseSettings> pulseSpamDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                pulseSpamDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                pulseSpamDatabaseSettings.Value.DatabaseName);

            _TareaProgramacionCollection = mongoDatabase.GetCollection<TareaProgramacion>(
                pulseSpamDatabaseSettings.Value.TareaProgramacionCollectionName);
        }

        public async Task<List<TareaProgramacion>> GetAsync() =>
            await _TareaProgramacionCollection.Find(_ => true).ToListAsync();

        public async Task<TareaProgramacion?> GetAsync(string id) =>
            await _TareaProgramacionCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TareaProgramacion nuevoTareaProgramacion) =>
            await _TareaProgramacionCollection.InsertOneAsync(nuevoTareaProgramacion);

        public async Task UpdateAsync(string id, TareaProgramacion trackingUpd) =>
            await _TareaProgramacionCollection.ReplaceOneAsync(x => x.Id == id, trackingUpd);

        public async Task RemoveAsync(string id) =>
            await _TareaProgramacionCollection.DeleteOneAsync(x => x.Id == id);

    }
}
