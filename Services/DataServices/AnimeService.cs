using MongoDB.Driver;
using MovieApi.Models;
using MovieApi.Service;

namespace MovieApi.Services.DataServices
{
    /// <summary>
    /// Anime MongoDB service
    /// </summary>
    public class AnimeService
    {
        private readonly IMongoCollection<Anime> _collection;

        /// <summary>
        /// Constructor
        /// </summary>
        public AnimeService(IConfiguration config, MongoDbConnectionService connectionService)
        {
            var collectionName = config.GetSection("MongoDB:CollectionContentName").Get<string>();

            _collection = connectionService.Database.GetCollection<Anime>(collectionName);
        }

        /// <summary>
        /// Get Anime
        /// </summary>
        public async Task<Anime> GetAsync(string id)
        {
            var model = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return model;
        }

        /// <summary>
        /// Create Anime
        /// </summary>
        public async Task<Anime> CreateAsync(Anime model)
        {
            await _collection.InsertOneAsync(model);
            return model;
        }

        /// <summary>
        /// Update Anime
        /// </summary>
        public async Task<Anime> UpdateAsync(string id, Anime model)
        {
            model = await _collection.FindOneAndReplaceAsync(x => x.Id == id, model, new() { ReturnDocument = ReturnDocument.After });
            return model;
        }

        /// <summary>
        /// Delete Anime
        /// </summary>
        public async Task<bool> DeleteAsync(string id)
        {
            var model = await _collection.FindOneAndDeleteAsync(x => x.Id == id);
            return model != null;
        }
    }
}
