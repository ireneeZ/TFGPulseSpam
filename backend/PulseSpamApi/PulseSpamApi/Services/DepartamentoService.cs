using Microsoft.Extensions.Options;
using PulseSpamApi.Models;
using MongoDB.Driver;
using PulseSpamApi.Interfaces;

namespace PulseSpamApi.Services
{
    public class DepartamentoService: IDepartamentoService
    {
        private readonly IMongoCollection<Departamento> _departamentoCollection;

        public DepartamentoService(
            IOptions<PulseSpamDatabaseSettings> pulseSpamDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                pulseSpamDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                pulseSpamDatabaseSettings.Value.DatabaseName);

            _departamentoCollection = mongoDatabase.GetCollection<Departamento>(
                pulseSpamDatabaseSettings.Value.DepartamentoCollectionName);
        }

        public async Task<List<Departamento>> GetAsync() =>
            await _departamentoCollection.Find(_ => true).ToListAsync();

        public async Task<Departamento?> GetAsync(string id) =>
            await _departamentoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Departamento nuevoDepartamento) =>
            await _departamentoCollection.InsertOneAsync(nuevoDepartamento);

        public async Task UpdateAsync(string id, Departamento dptoUpd) =>
            await _departamentoCollection.ReplaceOneAsync(x => x.Id == id, dptoUpd);

        public async Task RemoveAsync(string id) =>
            await _departamentoCollection.DeleteOneAsync(x => x.Id == id);
    }
}
