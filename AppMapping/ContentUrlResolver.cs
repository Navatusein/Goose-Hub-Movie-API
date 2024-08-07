﻿using AutoMapper;
using MovieApi.Controllers;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Service;
using MovieApi.Services.DataServices;

namespace MovieApi.AppMapping
{
    /// <summary>
    /// ContentUrlResolver
    /// </summary>
    public class ContentUrlResolver : IMemberValueResolver<object, object, string?, string?>
    {
        private static Serilog.ILogger Logger => Serilog.Log.ForContext<ContentUrlResolver>();

        private readonly MinioService _minioService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContentUrlResolver(MinioService minioService)
        {
            _minioService = minioService;
        }

        /// <summary>
        /// Resolve
        /// </summary>
        public string? Resolve(object source, object destination, string? sourceMember, string? destMember, ResolutionContext context)
        {
            if (sourceMember == null)
                return null;

            return _minioService.GetContentUrlAsync(sourceMember).Result;
        }
    }
}
