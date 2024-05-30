using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace MovieApi.Models
{
    /// <summary>
    /// Model for serial
    /// </summary>
    public class Serial : Preview
    {
        /// <summary>
        /// Gets or Sets Time
        /// </summary>
        [Required]
        public string Time { get; set; } = null!;

        /// <summary>
        /// Gets or Sets TrailerUrl
        /// </summary>
        [Required]
        public string TrailerUrl { get; set; } = null!;

        /// <summary>
        /// Gets or Sets ScreenshotPath
        /// </summary>
        public List<string> ScreenshotPath { get; set; } = null!;

        /// <summary>
        /// Gets or Sets EpisodesCount
        /// </summary>
        [Required]
        public string? EpisodesCount { get; set; } = null!;

        /// <summary>
        /// Gets or Sets NextEpisodeDate
        /// </summary>
        [Required]
        public DateTime? NextEpisodeDate { get; set; }

        /// <summary>
        /// Gets or Sets Seasons
        /// </summary>
        [Required]
        public List<Season> Seasons { get; set; } = null!;
    }
}
