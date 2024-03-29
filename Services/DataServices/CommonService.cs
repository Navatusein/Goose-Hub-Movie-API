using MongoDB.Bson;
using MongoDB.Driver;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Service;
using System.Xml.Linq;

namespace MovieApi.Services.DataServices
{
    /// <summary>
    /// Common MongoDB service
    /// </summary>
    public class CommonService
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<CommonService>();

        private readonly IMongoCollection<Preview> _collection;

        /// <summary>
        /// Constructor
        /// </summary>
        public CommonService(IConfiguration config, MongoDbConnectionService connectionService)
        {
            var collectionName = config.GetSection("MongoDB:CollectionContentName").Get<string>();

            _collection = connectionService.Database.GetCollection<Preview>(collectionName);
        }

        /// <summary>
        /// Get DirectedBy
        /// </summary>
        public async Task<List<string>> GetDirectedByAsync(string query)
        {
            var filter = Builders<Preview>.Filter.Regex("DirectedBy", new BsonRegularExpression(query, "i"));
            var field = new StringFieldDefinition<Preview, string>("DirectedBy");
            var result = await _collection.Distinct(field, filter).ToListAsync();
            return result;
        }

        /// <summary>
        /// Get Genres
        /// </summary>
        public async Task<List<string>> GetGenresAsync()
        {
            var filter = FilterDefinition<Preview>.Empty;
            var field = new StringFieldDefinition<Preview, string>("Genres");
            var result = await _collection.Distinct(field, filter).ToListAsync();
            return result;
        }

        /// <summary>
        /// Get Years
        /// </summary>
        public async Task<List<int>> GetYearsAsync()
        {
            var filter = FilterDefinition<Preview>.Empty;
            var field = new StringFieldDefinition<Preview, int>("Release.Year");
            var result = await _collection.Distinct(field, filter).ToListAsync();
            return result;
        }

        /// <summary>
        /// Get Preview by Query
        /// </summary>
        public async Task<List<Preview>> GetPreviewsByQueryAsync(QueryDto queryDto)
        {
            var filter = GetQueryFilter(queryDto);
            var result = await _collection.Find(filter)
                .Skip((queryDto.Page - 1) * queryDto.PageSize)
                .Limit(queryDto.PageSize)
                .ToListAsync();

            return result;
        }

        /// <summary>
        /// Count Preview by Query
        /// </summary>
        public async Task<long> CountPreviewsByQueryAsync(QueryDto queryDto)
        {
            var filter = GetQueryFilter(queryDto);
            var result = await _collection.CountDocumentsAsync(filter);

            return result;
        }

        /// <summary>
        /// Get Preview by Franchise Id
        /// </summary>
        public async Task<List<Preview>> GetPreviewsByFranchiseAsync(string franchiseId)
        {
            var filter = Builders<Preview>.Filter.Eq("FranchiseId", franchiseId);

            var result = await _collection.Find(filter).ToListAsync();
            return result;
        }

        /// <summary>
        /// Get Preview by Ids
        /// </summary>
        public async Task<List<Preview>> GetPreviewsByIds(List<string> ids)
        {
            var filter = Builders<Preview>.Filter.In("Id", ids);

            var result = await _collection.Find(filter).ToListAsync();
            return result;
        }

        /// <summary>
        /// Check content exist
        /// </summary>
        public async Task<bool> GetPreviewsByIds(string id)
        {
            var filter = Builders<Preview>.Filter.Eq("Id", id);
            var result = await _collection.Find(filter).FirstOrDefaultAsync();

            return result != null;
        }

        /// <summary>
        /// Get filter to filter by query
        /// </summary>
        private FilterDefinition<Preview> GetQueryFilter(QueryDto queryDto)
        {
            var filterBuilder = Builders<Preview>.Filter;
            var filters = new List<FilterDefinition<Preview>>();

            if (queryDto.ContentType != null)
                filters.Add(filterBuilder.Eq("ContentType", queryDto.ContentType));

            if (queryDto.Status != null)
                filters.Add(filterBuilder.Eq("Status", queryDto.Status));

            if (queryDto.Genres.Count != 0)
                filters.Add(filterBuilder.All("Genres", queryDto.Genres));

            if (queryDto.YearStart != null && queryDto.YearEnd != null)
                filters.Add(filterBuilder.Gte("Release.Year", queryDto.YearStart) & filterBuilder.Lte("Release.Year", queryDto.YearEnd));

            if (queryDto.Query != null)
                filters.Add(filterBuilder.Regex("Name", new BsonRegularExpression(queryDto.Query, "i")));

            var combinedFilter = filters.Count != 0 ? filterBuilder.And(filters) : filterBuilder.Empty;

            return combinedFilter;
        }
    }
}
