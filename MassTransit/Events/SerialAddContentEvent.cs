using MassTransit;
using MovieApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.MassTransit.Events
{
    /// <summary>
    /// Model for SerialAddContentEvent
    /// </summary>
    [EntityName("movie-api-serial-add-content")]
    [MessageUrn("SerialAddContentEvent")]
    public class SerialAddContentEvent
    {
        /// <summary>
        /// Gets or Sets EpisodeId
        /// </summary>
        [Required]
        public string EpisodeId { get; set; } = null!;

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
