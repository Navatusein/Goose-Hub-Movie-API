﻿using MongoDB.Bson;
using MongoDB.Driver;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Service;

namespace MovieApi.Services.DataServices
{
    /// <summary>
    /// Common MongoDB service
    /// </summary>
    public class PreviewService
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<PreviewService>();

        private readonly IMongoCollection<Preview> _collection;

        /// <summary>
        /// Constructor
        /// </summary>
        public PreviewService(IConfiguration config, MongoDbConnectionService connectionService)
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
        public async Task<List<string>> GetGenresAsync(ContentTypeEnum? contentType)
        {
            var filter = FilterDefinition<Preview>.Empty;

            if (contentType != null)
                filter = Builders<Preview>.Filter.Eq("ContentType", contentType);

            var field = new StringFieldDefinition<Preview, string>("Genres");
            var result = await _collection.Distinct(field, filter).ToListAsync();
            return result;
        }

        /// <summary>
        /// Get Years
        /// </summary>
        public async Task<List<int>> GetYearsAsync(ContentTypeEnum? contentType)
        {
            var filter = FilterDefinition<Preview>.Empty;

            if (contentType != null)
                filter = Builders<Preview>.Filter.Eq("ContentType", contentType);

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
            var sort = Builders<Preview>.Sort.Descending("Release");
            var result = await _collection.Find(filter)
                .Skip((queryDto.Page - 1) * queryDto.PageSize)
                .Limit(queryDto.PageSize)
                .Sort(sort)
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
        public async Task<List<Preview>> GetPreviewsByIdsAsync(List<string> ids)
        {
            var filter = Builders<Preview>.Filter.In("Id", ids);

            var result = await _collection.Find(filter).ToListAsync();
            return result;
        }

        /// <summary>
        /// Get Preview by Id
        /// </summary>
        public async Task<Preview> GetPreviewsByIdAsync(string id)
        {
            var filter = Builders<Preview>.Filter.Eq("Id", id);

            var result = await _collection.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// Check content exist
        /// </summary>
        public async Task<bool> ContentExistAsync(string id)
        {
            var filter = Builders<Preview>.Filter.Eq("Id", id);
            var result = await _collection.Find(filter).FirstOrDefaultAsync();

            return result != null;
        }

        /// <summary>
        /// Check content exist
        /// </summary>
        public async Task<bool> EpisodeExistAsync(string id)
        {
            var filter = Builders<Preview>.Filter.Or(
                Builders<Preview>.Filter.ElemMatch("Seasons.Episodes", Builders<Preview>.Filter.Eq("Id", id)), 
                Builders<Preview>.Filter.ElemMatch("Episodes", Builders<Preview>.Filter.Eq("Id", id)));

            var result = await _collection.Find(filter).FirstOrDefaultAsync();

            return result != null;
        }

        /// <summary>
        /// Add Screenshot
        /// </summary>
        public async Task<bool> AddScreenshotAsync(string id, string filePath)
        {
            var filter = Builders<Preview>.Filter.Eq("Id", id);
            var update = Builders<Preview>.Update.Push("ScreenshotPath", filePath);
            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Remove Screenshot
        /// </summary>
        public async Task<bool> RemoveScreenshotAsync(string id, string filePath)
        {
            var filter = Builders<Preview>.Filter.Eq("Id", id);
            var update = Builders<Preview>.Update.Pull("ScreenshotPath", filePath);

            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;
        }


        /// <summary>
        /// Add Poster
        /// </summary>
        public async Task<bool> AddPosterAsync(string id, string filePath)
        {
            var filter = Builders<Preview>.Filter.Eq("Id", id);
            var update = Builders<Preview>.Update.Set("PosterPath", filePath);

            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Remove Poster
        /// </summary>
        public async Task<bool> RemovePosterAsync(string id)
        {
            var filter = Builders<Preview>.Filter.Eq("Id", id);
            var update = Builders<Preview>.Update.Set<string?>("PosterPath", null);

            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Add Banner
        /// </summary>
        public async Task<bool> AddBannerAsync(string id, string filePath)
        {
            var filter = Builders<Preview>.Filter.Eq("Id", id);
            var update = Builders<Preview>.Update.Set("BannerPath", filePath);

            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Remove Banner
        /// </summary>
        public async Task<bool> RemoveBannerAsync(string id)
        {
            var filter = Builders<Preview>.Filter.Eq("Id", id);
            var update = Builders<Preview>.Update.Set<string?>("BannerPath", null);

            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;
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

            if (queryDto.Statuses.Count != 0)
                filters.Add(filterBuilder.In("Status", queryDto.Statuses));

            if (queryDto.AnimeTypes.Count != 0)
                filters.Add(filterBuilder.In("AnimeType", queryDto.AnimeTypes));

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
