using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    /// <summary>
    /// Model for serial season
    /// </summary>
    public class SeasonDto
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [Required]
        public string Id { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Index
        /// </summary>
        [Required]
        public int Index { get; set; } 

        /// <summary>
        /// Gets or Sets Episodes
        /// </summary>
        public List<EpisodeDto> Episodes { get; set; } = null!;
    }
}
