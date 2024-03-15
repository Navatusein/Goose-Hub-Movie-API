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
        private readonly IMongoCollection<SerialService> _collection;

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
        public async Task<Serial> GetAsync(string id)
        {
            var model = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
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
            model = await _collection.FindOneAndReplaceAsync(x => x.Id == id, model, new() { ReturnDocument = ReturnDocument.After });
            return model;
        }

        /// <summary>
        /// Delete Serial
        /// </summary>
        public async Task<bool> DeleteAsync(string id)
        {
            var model = await _collection.FindOneAndDeleteAsync(x => x.Id == id);
            return model != null;
        }
    }
}
