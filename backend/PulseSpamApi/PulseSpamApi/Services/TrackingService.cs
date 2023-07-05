using Microsoft.Extensions.Options;
using PulseSpamApi.Models;
using MongoDB.Driver;
using PulseSpamApi.Interfaces;

namespace PulseSpamApi.Services
{
    public class TrackingService: ITrackingService
    {
        private readonly IMongoCollection<Tracking> _TrackingCollection;

        public TrackingService(
            IOptions<PulseSpamDatabaseSettings> pulseSpamDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                pulseSpamDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                pulseSpamDatabaseSettings.Value.DatabaseName);

            _TrackingCollection = mongoDatabase.GetCollection<Tracking>(
                pulseSpamDatabaseSettings.Value.TrackingCollectionName);
        }

        public async Task<List<Tracking>> GetAsync() =>
            await _TrackingCollection.Find(_ => true).ToListAsync();

        public async Task<Tracking?> GetAsync(string id) =>
            await _TrackingCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Tracking?> GetAsyncUserDate(string usuarioId, DateTime fecha) { 
            return await _TrackingCollection.Find(x => x.UsuarioId == usuarioId && 
            x.Fecha.Year == fecha.Year && x.Fecha.Month == fecha.Month && x.Fecha.Day == fecha.Day).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Tracking nuevoTracking) =>
            await _TrackingCollection.InsertOneAsync(nuevoTracking);

        public async Task UpdateAsync(string id, Tracking trackingUpd) =>
            await _TrackingCollection.ReplaceOneAsync(x => x.Id == id, trackingUpd);

        public async Task RemoveAsync(string id) =>
            await _TrackingCollection.DeleteOneAsync(x => x.Id == id);

    }
}
