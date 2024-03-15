using MongoDB.Driver;
using MovieApi.Models;
using MovieApi.Service;

namespace MovieApi.Services.DataServices
{
    /// <summary>
    /// Franchise MongoDB service
    /// </summary>
    public class FranchiseService
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<FranchiseService>();

        private readonly IMongoCollection<Franchise> _collection;

        /// <summary>
        /// Constructor
        /// </summary>
        public FranchiseService(IConfiguration config, MongoDbConnectionService connectionService)
        {
            var collectionName = config.GetSection("MongoDB:CollectionFranchiseName").Get<string>();

            _collection = connectionService.Database.GetCollection<Franchise>(collectionName);
        }

        /// <summary>
        /// Get Franchise
        /// </summary>
        public async Task<Franchise> GetAsync(string id)
        {
            var model = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return model;
        }

        /// <summary>
        /// Create Franchise
        /// </summary>
        public async Task<Franchise> CreateAsync(Franchise model)
        {
            await _collection.InsertOneAsync(model);
            return model;
        }

        /// <summary>
        /// Update Franchise
        /// </summary>
        public async Task<Franchise> UpdateAsync(string id, Franchise model)
        {
            model = await _collection.FindOneAndReplaceAsync(x => x.Id == id, model, new() { ReturnDocument = ReturnDocument.After });
            return model;
        }

        /// <summary>
        /// Delete Franchise
        /// </summary>
        public async Task<bool> DeleteAsync(string id)
        {
            var model = await _collection.FindOneAndDeleteAsync(x => x.Id == id);
            return model != null;
        }
    }
}
