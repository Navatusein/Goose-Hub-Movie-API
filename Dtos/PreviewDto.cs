using MovieApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    /// <summary>
    /// Parent model for anime, serial, movie
    /// </summary>
    public class PreviewDto
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or Sets FranchiseId
        /// </summary>
        public string? FranchiseId { get; set; }

        /// <summary>
        /// Gets or Sets DataType
        /// </summary>
        [Required]
        public DataTypeEnum DataType { get; set; }

        /// <summary>
        /// Gets or Sets ContentType
        /// </summary>
        [Required]
        public ContentTypeEnum ContentType { get; set; }

        /// <summary>
        /// Gets or Sets PosterPath
        /// </summary>
        public string? PosterPath { get; set; }

        /// <summary>
        /// Gets or Sets PosterUrl
        /// </summary>
        public string? PosterUrl { get; set; }

        /// <summary>
        /// Gets or Sets BannerPath
        /// </summary>
        public string? BannerPath { get; set; }

        /// <summary>
        /// Gets or Sets BannerUrl
        /// </summary>
        public string? BannerUrl { get; set; }

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

        /// <summary>
        /// Gets or Sets Genres
        /// </summary>
        [Required]
        public List<string> Genres { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Release
        /// </summary>
        [Required]
        public DateOnly Release { get; set; }

        /// <summary>
        /// Gets or Sets AgeRestriction
        /// </summary>
        [Required]
        public string AgeRestriction { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Country
        /// </summary>
        [Required]
        public string Country { get; set; } = null!;


        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [Required]
        public StatusEnum Status { get; set; }

        /// <summary>
        /// Gets or Sets DirectedBy
        /// </summary>
        [Required]
        public List<string> DirectedBy { get; set; } = null!;
    }
}
