using Minio.DataModel.Notification;
using MongoDB.Driver;
using MovieApi.Models;
using MovieApi.Service;

namespace MovieApi.Services.DataServices
{
    /// <summary>
    /// Movie MongoDB service
    /// </summary>
    public class MovieService
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<MovieService>();

        private readonly IMongoCollection<Movie> _collection;

        /// <summary>
        /// Constructor
        /// </summary>
        public MovieService(IConfiguration config, MongoDbConnectionService connectionService)
        {
            var collectionName = config.GetSection("MongoDB:CollectionContentName").Get<string>();

            _collection = connectionService.Database.GetCollection<Movie>(collectionName);
        }

        /// <summary>
        /// Get Movie
        /// </summary>
        public async Task<Movie> GetByIdAsync(string id)
        {
            var filter = Builders<Movie>.Filter.Eq("Id", id);
            var model = await _collection.Find(filter).FirstOrDefaultAsync();
            return model;
        }

        /// <summary>
        /// Create Movie
        /// </summary>
        public async Task<Movie> CreateAsync(Movie model)
        {
            await _collection.InsertOneAsync(model);
            return model;
        }

        /// <summary>
        /// Update Movie
        /// </summary>
        public async Task<Movie> UpdateAsync(string id, Movie model)
        {
            var filter = Builders<Movie>.Filter.Eq("Id", id);
            var options = new FindOneAndReplaceOptions<Movie>()
            {
                ReturnDocument = ReturnDocument.After
            };

            model = await _collection.FindOneAndReplaceAsync(filter, model, options);
            return model;
        }

        /// <summary>
        /// Delete Movie
        /// </summary>
        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<Movie>.Filter.Eq("Id", id);
            var model = await _collection.FindOneAndDeleteAsync(filter);
            return model != null;
        }

        /// <summary>
        /// Add Content
        /// </summary>
        public async Task<bool> AddContentAsync(string id, Content content)
        {
            var filter = Builders<Movie>.Filter.Eq("Id", id);
            var update = Builders<Movie>.Update.Push("Content", content);
            var options = new FindOneAndUpdateOptions<Movie>()
            {
                ReturnDocument = ReturnDocument.After
            };

            var model = await _collection.FindOneAndUpdateAsync(filter, update, options);

            return model != null;
        }
    }
}
