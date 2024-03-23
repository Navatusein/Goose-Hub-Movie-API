using MongoDB.Bson;
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
        /// Get Franchise By Query
        /// </summary>
        public async Task<List<Franchise>> GetByQueryAsync(string query)
        {
            var filter = Builders<Franchise>.Filter.Regex("Name", new BsonRegularExpression(query, "i"));
            var models = await _collection.Find(filter).ToListAsync();
            return models;
        }

        /// <summary>
        /// Get Franchise
        /// </summary>
        public async Task<Franchise> GetAsync(string id)
        {
            var filter = Builders<Franchise>.Filter.Eq("Id", id);
            var model = await _collection.Find(filter).FirstOrDefaultAsync();
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
            var filter = Builders<Franchise>.Filter.Eq("Id", id);
            var options = new FindOneAndReplaceOptions<Franchise>()
            {
                ReturnDocument = ReturnDocument.After
            };

            model = await _collection.FindOneAndReplaceAsync(filter, model, options);
            return model;
        }

        /// <summary>
        /// Delete Franchise
        /// </summary>
        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<Franchise>.Filter.Eq("Id", id);
            var model = await _collection.FindOneAndDeleteAsync(filter);
            return model != null;
        }
    }
}
