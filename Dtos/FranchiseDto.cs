using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace MovieApi.Dtos
{
    /// <summary>
    /// Model for franchise
    /// </summary>
    public class FranchiseDto
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [Required]
        public string Description { get; set; } = null!;
    }
}
