using MovieApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace MovieApi.Dtos
{
    /// <summary>
    /// Dto for Anime
    /// </summary>
    public class AnimeDto : PreviewDto
    {
        /// <summary>
        /// Gets or Sets Time
        /// </summary>
        [Required]
        public string Time { get; set; } = null!;

        /// <summary>
        /// Gets or Sets TrailerUrl
        /// </summary>
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
        /// Gets or Sets Type
        /// </summary>
        [Required]
        public AnimeTypeEnum AnimeType { get; set; }

        /// <summary>
        /// Gets or Sets Studio
        /// </summary>
        [Required]
        public string Studio { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Content
        /// </summary>
        public List<ContentDto>? Content { get; set; }

        /// <summary>
        /// Gets or Sets Episodes
        /// </summary>
        public List<EpisodeDto> Episodes { get; set; } = null!;
    }
}
