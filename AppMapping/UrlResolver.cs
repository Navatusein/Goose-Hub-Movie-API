using AutoMapper;
using MovieApi.Controllers;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Service;
using MovieApi.Services.DataServices;

namespace MovieApi.AppMapping
{
    /// <summary>
    /// PosterUrlResolver
    /// </summary>
    public class PosterUrlResolver : IMemberValueResolver<object, object, string, string>
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<PosterUrlResolver>();

        private readonly MinioService _minioService;

        /// <summary>
        /// Constructor
        /// </summary>
        public PosterUrlResolver(MinioService minioService)
        {
            _minioService = minioService;
        }

        /// <summary>
        /// Resolve
        /// </summary>
        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            return _minioService.GetScreenshoPresignedUrl(sourceMember).Result;
        }
    }
}
