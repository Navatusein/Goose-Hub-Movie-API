using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    /// <summary>
    /// Dto for Content
    /// </summary>
    public class EpisodeDto
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or Sets Index
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or Sets ContentPath
        /// </summary>
        public string? ContentPath { get; set; }

        /// <summary>
        /// Gets or Sets ContentUrl
        /// </summary>
        public string? ContentUrl { get; set; }
    }
}
