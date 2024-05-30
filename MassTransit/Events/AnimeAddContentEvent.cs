using MassTransit;
using MovieApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.MassTransit.Events
{
    /// <summary>
    /// Model for AnimeAddContentEvent
    /// </summary>
    [EntityName("movie-api-anime-add-content")]
    [MessageUrn("AnimeAddContentEvent")]
    public class AnimeAddContentEvent
    {
        /// <summary>
        /// Gets or Sets ContentId
        /// </summary>
        [Required]
        public string ContentId { get; set; } = null!;

        /// <summary>
        /// Gets or Sets IsEpisode
        /// </summary>
        [Required]
        public bool IsEpisode { get; set; }

        /// <summary>
        /// Gets or Sets ContentFileName
        /// </summary>
        [Required]
        public string ContentFileName { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Quality
        /// </summary>
        [Required]
        public ContentQuality Quality { get; set; }
    }
}
