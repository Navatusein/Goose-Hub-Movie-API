using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace MovieApi.Dto
{
    /// <summary>
    /// Model for serial
    /// </summary>
    public class SerialDto : PreviewDto
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
        /// Gets or Sets Screenshots
        /// </summary>
        public List<string> Screenshots { get; set; } = null!;

        /// <summary>
        /// Gets or Sets ScreenshotIds
        /// </summary>
        public List<string> ScreenshotIds { get; set; } = null!;

        /// <summary>
        /// Gets or Sets EpisodesCount
        /// </summary>
        [Required]
        public string EpisodesCount { get; set; } = null!;

        /// <summary>
        /// Gets or Sets NextEpisodeDate
        /// </summary>
        [Required]
        public DateTime NextEpisodeDate { get; set; }

        /// <summary>
        /// Gets or Sets Seasons
        /// </summary>
        [Required]
        public List<SeasonDto> Seasons { get; set; } = null!;
    }
}
