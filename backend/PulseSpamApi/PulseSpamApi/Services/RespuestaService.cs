using Microsoft.Extensions.Options;
using PulseSpamApi.Models;
using MongoDB.Driver;
using PulseSpamApi.Interfaces;

namespace PulseSpamApi.Services
{
    public class RespuestaService: IRespuestaService
    {
        private readonly IMongoCollection<Respuesta> _respuestaCollection;

        public RespuestaService(
            IOptions<PulseSpamDatabaseSettings> pulseSpamDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                pulseSpamDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                pulseSpamDatabaseSettings.Value.DatabaseName);

            _respuestaCollection = mongoDatabase.GetCollection<Respuesta>(
                pulseSpamDatabaseSettings.Value.RespuestaCollectionName);
        }

        public async Task<List<Respuesta>> GetAsync() =>
            await _respuestaCollection.Find(_ => true).ToListAsync();

        public async Task<Respuesta?> GetAsync(string id) =>
            await _respuestaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Respuesta nuevaRespuesta) =>
            await _respuestaCollection.InsertOneAsync(nuevaRespuesta);

        public async Task UpdateAsync(string id, Respuesta respuestaUpd) =>
            await _respuestaCollection.ReplaceOneAsync(x => x.Id == id, respuestaUpd);

        public async Task RemoveAsync(string id) =>
            await _respuestaCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Respuesta>> GetByQuestion(string preguntaId)
        {
            return await _respuestaCollection.Find(x => x.PreguntaId == preguntaId).ToListAsync();
        }
    }
}
