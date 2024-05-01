using MongoDB.Bson;
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
        public async Task<Anime> GetByIdAsync(string id)
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

        /// <summary>
        /// Add Content
        /// </summary>
        public async Task<bool> AddContentAsync(string id, string contentPath)
        {
            var filter = Builders<Anime>.Filter.Eq("Id", id);
            var update = Builders<Anime>.Update.Set("ContentPath", contentPath);
            var options = new FindOneAndUpdateOptions<Anime>()
            {
                ReturnDocument = ReturnDocument.After
            };

            var model = await _collection.FindOneAndUpdateAsync(filter, update, options);

            return model != null;
        }

        /// <summary>
        /// Add Content To Episode
        /// </summary>
        public async Task<bool> AddEpisodeContentAsync(string id, string contentPath)
        {
            var filter = Builders<Anime>.Filter.ElemMatch("Episodes", Builders<Episode>.Filter.Eq("Id", id));
            var update = Builders<Anime>.Update.Set("Episodes.$[e].ContentPath", contentPath);

            var options = new UpdateOptions
            {
                ArrayFilters = new[]
                {
                    new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("e._id", new ObjectId(id)))
                }
            };

            var result = await _collection.UpdateOneAsync(filter, update, options);

            if (result.IsModifiedCountAvailable && result.ModifiedCount > 0)
                return true;

            return false;
        }
    }
}
