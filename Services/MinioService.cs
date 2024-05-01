using MassTransit;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using MovieApi.Models;

namespace MovieApi.Service
{
    /// <summary>
    /// Service for work with Minio
    /// </summary>
    public class MinioService
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<MinioService>();

        private readonly IMinioClient _minioClient;

        private readonly string _endpoint;

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

            _endpoint = config.GetSection("MinIO:Endpoint").Get<string>()!;

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
        /// Generate url 
        /// </summary>
        public async Task<string> GetUrlAsync(string bucketName, string objectPath)
        {
            //var presignedGetObjectArgs = new PresignedGetObjectArgs()
            //        .WithBucket(bucketName)
            //        .WithObject(objectPath)
            //        .WithExpiry(60 * 60 * 24 * 7);

            //var presignedUrl = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs).ConfigureAwait(false);

            var presignedUrl = $"https://{_endpoint}/{bucketName}/{objectPath}";

            return presignedUrl;
        }

        /// <summary>
        /// Generate url for images
        /// </summary>
        public async Task<string> GetImageUrlAsync(string objectPath)
        {
            return await GetUrlAsync(_imageBucket, objectPath);
        }

        /// <summary>
        /// Generate url for content
        /// </summary>
        public async Task<string> GetContentUrlAsync(string objectPath)
        {
            return await GetUrlAsync(_contentBucket, objectPath);
        }

        /// <summary>
        /// Upload image to MinIO
        /// </summary>
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            string objectName = Guid.NewGuid().ToString() + extension;

            var stream = file.OpenReadStream();

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_imageBucket)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType(file.ContentType);

            await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);

            return objectName;
        }

        /// <summary>
        /// Remove image from MinIO
        /// </summary>
        public async Task RemoveImageAsync(string path)
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(_imageBucket)
                .WithObject(path);

            await _minioClient.RemoveObjectAsync(removeObjectArgs).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<List<string>?> GetContentFilesAsync(string fileName)
        {
            try
            {
                var lines = new List<string>();

                var getObjectArgs = new GetObjectArgs()
                    .WithBucket(_contentBucket)
                    .WithObject(fileName)
                    .WithCallbackStream((stream) =>
                    {
                        var reader = new StreamReader(stream);

                        string? line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            lines.Add(line);
                        }
                    });

                await _minioClient.GetObjectAsync(getObjectArgs).ConfigureAwait(false);

                return lines;
            }

            catch (ObjectNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task UploadContentFileLinesAsync(Stream stream, string fileName)
        {
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_contentBucket)
                .WithObject(fileName)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType("application/vnd.apple.mpegurl");

            await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<bool> GenerateHlsDescriptorAsync(string filePath, ContentQuality quality)
        {
            var fileExists = true;
            var lines = await GetContentFilesAsync(filePath);

            if (lines == null)
            {
                lines = new List<string>
                {
                    "#EXTM3U",
                    "#EXT-X-VERSION:3"
                };

                fileExists = false;
            }

            string[] newLines = null!;

            switch (quality)
            {
                case ContentQuality.SD:
                    newLines = new string[]
                    {
                    "#EXT-X-STREAM-INF:BANDWIDTH=1400000,RESOLUTION=854x480",
                    "480/480.m3u8"
                    };
                    break;

                case ContentQuality.HD:
                    newLines = new string[]
                    {
                    "#EXT-X-STREAM-INF:BANDWIDTH=2800000,RESOLUTION=1280x720",
                    "720/720.m3u8"
                    };
                    break;

                case ContentQuality.FullHD:
                    newLines = new string[]
                    {
                    "#EXT-X-STREAM-INF:BANDWIDTH=5000000,RESOLUTION=1920x1080",
                    "1080/1080.m3u8"
                    };
                    break;
            }

            var index = 2;

            for (; index < lines.Count; index += 2)
            {
                var contentQuality = Convert.ToInt32(lines[index + 1].Split("/")[0]);

                if (contentQuality < (int)quality)
                    break;
            }

            lines.InsertRange(index, newLines);

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            lines.ForEach(line =>
            {
                writer.WriteLine(line);
            });

            writer.Flush();
            stream.Position = 0;

            await UploadContentFileLinesAsync(stream, filePath);

            return fileExists;
        }
    }
}
