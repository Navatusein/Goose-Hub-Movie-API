using Minio;
using Minio.DataModel.Args;

namespace MovieApi.Service
{
    /// <summary>
    /// Service for work with Minio
    /// </summary>
    public class MinioService
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<MinioService>();

        private readonly IMinioClient _minioClient;

        private readonly string _imageBucket;
        private readonly string _contentBucket;

        /// <summary>
        /// Constructor
        /// </summary>
        public MinioService(IConfiguration config)
        {
            var endpoint = config.GetSection("MinIO:Endpoint").Get<string>();
            var useSsl = config.GetSection("MinIO:UseSSL").Get<bool>();
            var region = config.GetSection("MinIO:Region").Get<string>();
            var accessKey = config.GetSection("MinIO:AccessKey").Get<string>();
            var secretKey = config.GetSection("MinIO:SecretKey").Get<string>();

            _imageBucket = config.GetSection("MinIO:ImageBucket").Get<string>()!;
            _contentBucket = config.GetSection("MinIO:ContentBucket").Get<string>()!;
            
            _minioClient = new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .WithRegion(region)
                .WithSSL(useSsl)
                .Build();
        }

        /// <summary>
        /// Generate presigned url 
        /// </summary>
        public async Task<string> GetPresignedUrl(string bucketName, string objectPath)
        {
            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectPath)
                    .WithExpiry(60 * 60 * 24 * 7);

            var presignedUrl = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs).ConfigureAwait(false);

            return presignedUrl;
        }

        /// <summary>
        /// Generate presigned url for images
        /// </summary>
        public async Task<string> GetImagePresignedUrl(string objectPath)
        {
            return await GetPresignedUrl(_imageBucket, objectPath);
        }

        /// <summary>
        /// Generate presigned url for content
        /// </summary>
        public async Task<string> GetContentPresignedUrl(string objectPath)
        {
            return await GetPresignedUrl(_contentBucket, objectPath);
        }
    }
}
