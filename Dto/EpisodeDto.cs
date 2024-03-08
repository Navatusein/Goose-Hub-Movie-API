using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace MovieApi.Dto
{
    /// <summary>
    /// Model for serial season or anime episode
    /// </summary>
    public class EpisodeDto
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [Required]
        public string Id { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Index
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or Sets EpisodeUrl
        /// </summary>
        [Required]
        public string EpisodeUrl { get; set; } = null!;

        /// <summary>
        /// Gets or Sets EpisodeId
        /// </summary>
        [Required]
        public string EpisodeId { get; set; } = null!;
    }
}
