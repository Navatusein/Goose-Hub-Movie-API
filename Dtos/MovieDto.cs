using MovieApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    /// <summary>
    /// Dto for Movie
    /// </summary>
    public class MovieDto : PreviewDto
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
        public List<string> ScreenshotUrls { get; set; } = null!;

        /// <summary>
        /// Gets or Sets ScreenshotPath
        /// </summary>
        public List<string> ScreenshotPath { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Content
        /// </summary>
        public List<ContentDto>? Content { get; set; }
    }
}
