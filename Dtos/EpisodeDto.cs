using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    /// <summary>
    /// Model for serial season or anime episode
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
        /// Gets or Sets Content
        /// </summary>
        public List<ContentDto>? Content { get; set; }
    }
}
