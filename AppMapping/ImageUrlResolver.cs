﻿using AutoMapper;
using MovieApi.Controllers;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Service;
using MovieApi.Services.DataServices;

namespace MovieApi.AppMapping
{
    /// <summary>
    /// UrlResolver
    /// </summary>
    public class ImageUrlResolver : IMemberValueResolver<object, object, string, string>
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<ImageUrlResolver>();

        private readonly MinioService _minioService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ImageUrlResolver(MinioService minioService)
        {
            _minioService = minioService;
        }

        /// <summary>
        /// Resolve
        /// </summary>
        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            return _minioService.GetImagePresignedUrl(sourceMember).Result;
        }
    }
}
