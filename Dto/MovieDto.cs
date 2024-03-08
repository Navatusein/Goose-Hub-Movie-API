using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace MovieApi.Dto
{
    /// <summary>
    /// Model for movies
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
        public List<string> Screenshots { get; set; } = null!;

        /// <summary>
        /// Gets or Sets ScreenshotIds
        /// </summary>
        public List<string> ScreenshotIds { get; set; } = null!;

        /// <summary>
        /// Gets or Sets MovieUrl
        /// </summary>
        public string MovieUrl { get; set; } = null!;

        /// <summary>
        /// Gets or Sets MovieId
        /// </summary>
        public string MovieId { get; set; } = null!;
    }
}
