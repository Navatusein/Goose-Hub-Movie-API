using MassTransit;
using MovieApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.MassTransit.Events
{
    /// <summary>
    /// 
    /// </summary>
    [EntityName("movie-api-serial-add-content")]
    public class SerialAddContentEvent
    {
        /// <summary>
        /// Gets or Sets EpisodeId
        /// </summary>
        [Required]
        public string EpisodeId { get; set; } = null!;



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
