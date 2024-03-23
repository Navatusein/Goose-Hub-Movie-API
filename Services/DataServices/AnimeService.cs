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
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<AnimeService>();

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
            var filter = Builders<Anime>.Filter.Eq("Id", id);
            var model = await _collection.Find(filter).FirstOrDefaultAsync();
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
            var filter = Builders<Anime>.Filter.Eq("Id", id);
            var options = new FindOneAndReplaceOptions<Anime>()
            {
                ReturnDocument = ReturnDocument.After
            };

            model = await _collection.FindOneAndReplaceAsync(filter, model, options);
            return model;
        }

        /// <summary>
        /// Delete Anime
        /// </summary>
        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<Anime>.Filter.Eq("Id", id);
            var model = await _collection.FindOneAndDeleteAsync(filter);
            return model != null;
        }
    }
}
