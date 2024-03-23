﻿using MassTransit;
using MovieApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.MassTransit.Events
{
    /// <summary>
    /// 
    /// </summary>
    [EntityName("movie-api-movie-add-content")]
    public class MovieAddContentEvent
    {
        /// <summary>
        /// Gets or Sets MovieId
        /// </summary>
        [Required]
        public string MovieId { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Quality
        /// </summary>
        [Required]
        public ContentQuality Quality { get; set; }

        /// <summary>
        /// Gets or Sets Path
        /// </summary>
        [Required]
        public string Path { get; set; } = null!;
    }
}
