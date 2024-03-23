using MongoDB.Bson;
using MongoDB.Driver;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Service;
using System.Xml.Linq;

namespace MovieApi.Services.DataServices
{
    /// <summary>
    /// 
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

            //var indexes = _collection.Indexes.List().ToList();

            //if (!indexes.Any(x => x["name"].AsString == "NameTextIndex" )) 
            //{
            //    var key = Builders<Preview>.IndexKeys.Text(x => x.Name);
            //    var index = new CreateIndexModel<Preview>(key, new CreateIndexOptions { Name = "NameTextIndex" });
            //    _collection.Indexes.CreateOne(index);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<List<string>> GetDirectedByAsync(string query)
        {
            var filter = Builders<Preview>.Filter.Regex("DirectedBy", new BsonRegularExpression(query, "i"));
            var field = new StringFieldDefinition<Preview, string>("DirectedBy");
            var result = await _collection.Distinct(field, filter).ToListAsync();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<List<string>> GetGenresAsync()
        {
            var filter = FilterDefinition<Preview>.Empty;
            var field = new StringFieldDefinition<Preview, string>("Genres");
            var result = await _collection.Distinct(field, filter).ToListAsync();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<List<int>> GetYearsAsync()
        {
            var filter = FilterDefinition<Preview>.Empty;
            var field = new StringFieldDefinition<Preview, int>("Release.Year");
            var result = await _collection.Distinct(field, filter).ToListAsync();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<List<Preview>> GetPreviewsByQueryAsync(QueryDto queryDto)
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

            var result = await _collection.Find(combinedFilter)
                .Skip((queryDto.Page - 1) * queryDto.PageSize)
                .Limit(queryDto.PageSize)
                .ToListAsync();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<List<Preview>> GetPreviewsByFranchiseAsync(string franchiseId)
        {
            var filter = Builders<Preview>.Filter.Eq("FranchiseId", franchiseId);

            var result = await _collection.Find(filter).ToListAsync();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<List<Preview>> GetPreviewsByIds(List<string> ids)
        {
            var filter = Builders<Preview>.Filter.In("Id", ids);

            var result = await _collection.Find(filter).ToListAsync();
            return result;
        }

    }
}
