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
        private readonly IMongoCollection<Movie> _collection;

        /// <summary>
        /// Constructor
        /// </summary>
        public MovieService(IConfiguration config, MongoDbConnectionService connectionService)
        {
            var movieCollectionName = config.GetSection("MongoDB:CollectionContentName").Get<string>();

            _collection = connectionService.Database.GetCollection<Movie>(movieCollectionName);
        }

        /// <summary>
        /// Get Movie
        /// </summary>
        /// <param name="id">Movie id</param>
        /// <returns></returns>
        public async Task<Movie> GetAsync(string id)
        {
            var model = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return model;
        }

        /// <summary>
        /// Create Movie
        /// </summary>
        /// <param name="model">Movie model</param>
        /// <returns>Movie model</returns>
        public async Task<Movie> CreateAsync(Movie model)
        {
            await _collection.InsertOneAsync(model);
            return model;
        }

        /// <summary>
        /// Update Movie
        /// </summary>
        /// <param name="id">Movie id</param>
        /// <param name="model">Movie</param>
        /// <returns>Movie model</returns>
        public async Task<Movie> UpdateAsync(string id, Movie model)
        {
            model = await _collection.FindOneAndReplaceAsync(x => x.Id == id, model, new() { ReturnDocument = ReturnDocument.After });
            return model;
        }

        /// <summary>
        /// Delete Movie
        /// </summary>
        /// <param name="id">Movie id</param>
        /// <returns>Is deleted</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var model = await _collection.FindOneAndDeleteAsync(x => x.Id == id);
            return model != null;
        }
    }
}
