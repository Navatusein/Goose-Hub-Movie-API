using Minio;
using Minio.DataModel.Args;

namespace MovieApi.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class MinioService
    {
        private readonly IMinioClient _minioClient;

        private readonly string _contentBucket;
        private readonly string _screenshotBucket;

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

            _contentBucket = config.GetSection("MinIO:ContentBucket").Get<string>()!;
            _screenshotBucket = config.GetSection("MinIO:ScreenshotBucket").Get<string>()!;

            _minioClient = new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .WithRegion(region)
                .WithSSL(useSsl)
                .Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectPath"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="objectPath"></param>
        /// <returns></returns>
        public async Task<string> GetContentPresignedUrl(string objectPath)
        {
            return await GetPresignedUrl(_contentBucket, objectPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectPath"></param>
        /// <returns></returns>
        public async Task<string> GetScreenshoPresignedUrl(string objectPath)
        {
            return await GetPresignedUrl(_screenshotBucket, objectPath);
        }
    }
}
