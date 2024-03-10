using AutoMapper;
using MovieApi.Controllers;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Service;
using MovieApi.Services.DataServices;

namespace MovieApi.AppMapping
{
    /// <summary>
    /// ScreenshotUrlResolver
    /// </summary>
    public class ScreenshotUrlResolver : IMemberValueResolver<object, object, List<string>, List<string>>
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<ScreenshotUrlResolver>();

        private readonly MinioService _minioService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ScreenshotUrlResolver(MinioService minioService)
        {
            _minioService = minioService;
        }

        /// <summary>
        /// Resolve
        /// </summary>
        public List<string> Resolve(object source, object destination, List<string> sourceMember, List<string> destMember, ResolutionContext context)
        {
            return sourceMember.Select(x => _minioService.GetScreenshoPresignedUrl(x).Result).ToList();
        }
    }
}
