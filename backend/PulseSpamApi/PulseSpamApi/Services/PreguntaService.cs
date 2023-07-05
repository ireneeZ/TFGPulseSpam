using Microsoft.Extensions.Options;
using PulseSpamApi.Models;
using MongoDB.Driver;
using PulseSpamApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PulseSpamApi.Services
{
    public class PreguntaService: IPreguntaService
    {
        private readonly IMongoCollection<Pregunta> _preguntaCollection;

        public PreguntaService(
            IOptions<PulseSpamDatabaseSettings> pulseSpamDatabaseSettings)
        {
            var mongoClient = new MongoClient(
               pulseSpamDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                pulseSpamDatabaseSettings.Value.DatabaseName);

            _preguntaCollection = mongoDatabase.GetCollection<Pregunta>(
                pulseSpamDatabaseSettings.Value.PreguntaCollectionName);
        }

        public async Task<List<Pregunta>> GetAsync() =>
            await _preguntaCollection.Find(_ => true).ToListAsync();

        public async Task<Pregunta?> GetAsync(string id) =>
            await _preguntaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Pregunta nuevaPregunta) =>
            await _preguntaCollection.InsertOneAsync(nuevaPregunta);

        public async Task UpdateAsync(string id, Pregunta preguntaUpd) =>
            await _preguntaCollection.ReplaceOneAsync(x => x.Id == id, preguntaUpd);

        public async Task RemoveAsync(string id) =>
            await _preguntaCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Pregunta>> GetByCategory(String categoriaId) =>
            await _preguntaCollection.Find(x => x.CategoriaId == categoriaId).ToListAsync();
    }
}
