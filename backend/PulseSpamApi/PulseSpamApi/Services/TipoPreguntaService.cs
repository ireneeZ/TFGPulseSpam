using Microsoft.Extensions.Options;
using PulseSpamApi.Models;
using MongoDB.Driver;
using PulseSpamApi.Interfaces;

namespace PulseSpamApi.Services
{
    public class TipoPreguntaService: ITipoPreguntaService
    {
        private readonly IMongoCollection<TipoPregunta> _tipoPreguntaCollection;

        public TipoPreguntaService(
            IOptions<PulseSpamDatabaseSettings> pulseSpamDatabaseSettings)
        {
            var mongoClient = new MongoClient(
               pulseSpamDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                pulseSpamDatabaseSettings.Value.DatabaseName);

            _tipoPreguntaCollection = mongoDatabase.GetCollection<TipoPregunta>(
                pulseSpamDatabaseSettings.Value.TipoPreguntaCollectionName);
        }

        public async Task<List<TipoPregunta>> GetAsync() =>
            await _tipoPreguntaCollection.Find(_ => true).ToListAsync();

        public async Task<TipoPregunta?> GetAsync(string id) =>
            await _tipoPreguntaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TipoPregunta tipoPregunta) =>
            await _tipoPreguntaCollection.InsertOneAsync(tipoPregunta);

        public async Task UpdateAsync(string id, TipoPregunta tipoPreguntaUpd) =>
            await _tipoPreguntaCollection.ReplaceOneAsync(x => x.Id == id, tipoPreguntaUpd);

        public async Task RemoveAsync(string id) =>
            await _tipoPreguntaCollection.DeleteOneAsync(x => x.Id == id);
    }
}
