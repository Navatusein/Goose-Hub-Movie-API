using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieApi.Models;
using MovieApi.Service;

namespace MovieApi.Services.DataServices
{
    /// <summary>
    /// Serial MongoDB service
    /// </summary>
    public class SerialService
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<SerialService>();

        private readonly IMongoCollection<Serial> _collection;

        /// <summary>
        /// Constructor
        /// </summary>
        public SerialService(IConfiguration config, MongoDbConnectionService connectionService)
        {
            var collectionName = config.GetSection("MongoDB:CollectionContentName").Get<string>();

            _collection = connectionService.Database.GetCollection<Serial>(collectionName);
        }

        /// <summary>
        /// Get Serial
        /// </summary>
        public async Task<Serial> GetByIdAsync(string id)
        {
            var filter = Builders<Serial>.Filter.Eq("Id", id);
            var model = await _collection.Find(filter).FirstOrDefaultAsync();
            return model;
        }

        /// <summary>
        /// Create Serial
        /// </summary>
        public async Task<Serial> CreateAsync(Serial model)
        {
            await _collection.InsertOneAsync(model);
            return model;
        }

        /// <summary>
        /// Update Serial
        /// </summary>
        public async Task<Serial> UpdateAsync(string id, Serial model)
        {
            var filter = Builders<Serial>.Filter.Eq("Id", id);
            var options = new FindOneAndReplaceOptions<Serial>()
            {
                ReturnDocument = ReturnDocument.After
            };

            model = await _collection.FindOneAndReplaceAsync(filter, model, options);
            return model;
        }

        /// <summary>
        /// Delete Serial
        /// </summary>
        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<Serial>.Filter.Eq("Id", id);
            var model = await _collection.FindOneAndDeleteAsync(filter);
            return model != null;
        }

        /// <summary>
        /// Add Content To Episode
        /// </summary>
        public async Task<bool> AddEpisodeContentAsync(string id, string contentPath)
        {
            var filter = Builders<Serial>.Filter.ElemMatch("Seasons.Episodes", Builders<Episode>.Filter.Eq("Id", id));
            var update = Builders<Serial>.Update.Set("Seasons.$[s].Episodes.$[e].ContentPath", contentPath);

            var options = new UpdateOptions
            {
                ArrayFilters = new[]
                {
                    new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("s.Episodes._id", new ObjectId(id))),
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
