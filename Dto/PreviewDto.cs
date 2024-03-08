using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace MovieApi.Dto
{
    /// <summary>
    /// Content status enum
    /// </summary>
    public enum StatusEnum
    {
        /// <summary>
        /// Completed
        /// </summary>
        Completed = 1,

        /// <summary>
        /// Announcement
        /// </summary>
        Announcement = 2,

        /// <summary>
        /// Ongoing
        /// </summary>
        Ongoing = 3,

        /// <summary>
        /// Paused
        /// </summary>
        Paused = 4
    }

    /// <summary>
    /// Content data type enum
    /// </summary>
    public enum DataTypeEnum
    {

        /// <summary>
        /// Movie
        /// </summary>
        Movie = 1,

        /// <summary>
        /// Serial
        /// </summary>
        Serial = 2,

        /// <summary>
        /// Anime
        /// </summary>
        Anime = 3
    }

    /// <summary>
    /// Content type enum
    /// </summary>
    public enum ContentTypeEnum
    {

        /// <summary>
        /// Moive
        /// </summary>
        Moive = 1,

        /// <summary>
        /// Serial
        /// </summary>
        Serial = 2,

        /// <summary>
        /// Cartoon
        /// </summary>
        Cartoon = 3,

        /// <summary>
        /// Anime
        /// </summary>
        Anime = 4
    }

    /// <summary>
    /// Parent model for anime, serial, movie
    /// </summary>
    public class PreviewDto
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [Required]
        public string Id { get; set; } = null!;

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
        /// Gets or Sets PosterUrl
        /// </summary>
        [Required]
        public string PosterUrl { get; set; } = null!;

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
        public DateTime Release { get; set; }

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
