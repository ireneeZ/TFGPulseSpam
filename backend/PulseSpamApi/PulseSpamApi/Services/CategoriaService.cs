using Microsoft.Extensions.Options;
using PulseSpamApi.Models;
using MongoDB.Driver;
using PulseSpamApi.Interfaces;

namespace PulseSpamApi.Services
{
    public class CategoriaService: ICategoriaService
    {
        private readonly IMongoCollection<Categoria> _categoriaCollection;

        public CategoriaService(
            IOptions<PulseSpamDatabaseSettings> pulsespamDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                pulsespamDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                pulsespamDatabaseSettings.Value.DatabaseName);

            _categoriaCollection = mongoDatabase.GetCollection<Categoria>(
                pulsespamDatabaseSettings.Value.CategoriaCollectionName);
        }

        public async Task<List<Categoria>> GetAsync() =>
            await _categoriaCollection.Find(_ => true).ToListAsync();

        public async Task<Categoria?> GetAsync(string id) =>
            await _categoriaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Categoria nuevaCategoria) =>
            await _categoriaCollection.InsertOneAsync(nuevaCategoria);

        public async Task UpdateAsync(string id, Categoria categoriaUpd) =>
            await _categoriaCollection.ReplaceOneAsync(x => x.Id == id, categoriaUpd);

        public async Task RemoveAsync(string id) =>
            await _categoriaCollection.DeleteOneAsync(x => x.Id == id);
    }
}
