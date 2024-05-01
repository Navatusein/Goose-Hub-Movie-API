using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace MovieApi.Models
{
    /// <summary>
    /// Model for serial season or anime episode
    /// </summary>
    public class Episode
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
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
    }
}
