using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
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
        /// Gets or Sets ScreenshotUrls
        /// </summary>
        public List<string> ScreenshotUrls { get; set; } = null!;

        /// <summary>
        /// Gets or Sets ScreenshotPath
        /// </summary>
        public List<string> ScreenshotPath { get; set; } = null!;

        /// <summary>
        /// Gets or Sets EpisodesCount
        /// </summary>
        public string? EpisodesCount { get; set; } = null!;

        /// <summary>
        /// Gets or Sets NextEpisodeDate
        /// </summary>
        public DateTime? NextEpisodeDate { get; set; }

        /// <summary>
        /// Gets or Sets Seasons
        /// </summary>
        [Required]
        public List<SeasonDto> Seasons { get; set; } = null!;
    }
}
