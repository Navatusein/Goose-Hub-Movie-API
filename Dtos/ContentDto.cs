using MovieApi.Models;

namespace MovieApi.Dtos
{
    /// <summary>
    /// Model for episode, movie data
    /// </summary>
    public class ContentDto
    {
        /// <summary>
        /// Gets or Sets Quality
        /// </summary>
        public ContentQuality Quality { get; set; }

        /// <summary>
        /// Gets or Sets Path
        /// </summary>
        public string Path { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Url
        /// </summary>
        public string Url { get; set; } = null!;
    }
}
