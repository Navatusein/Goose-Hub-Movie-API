using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace MovieApi.Models
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
        /// Movie
        /// </summary>
        Movie = 1,

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
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(Movie), typeof(Anime), typeof(Serial))]
    public class Preview
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or Sets FranchiseId
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
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
        /// Gets or Sets BannerPath
        /// </summary>
        public string? BannerPath { get; set; }

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

        /// <summary>
        /// Gets or Sets Studio
        /// </summary>
        [Required]
        public string Studio { get; set; } = null!;
    }
}
