using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace MovieApi.Models
{
    /// <summary>
    /// Anime type enum
    /// </summary>
    public enum AnimeTypeEnum
    {
        /// <summary>
        /// Special
        /// </summary>
        Special = 1,

        /// <summary>
        /// Film
        /// </summary>
        Film = 2,

        /// <summary>
        /// Ova
        /// </summary>
        Ova = 3,

        /// <summary>
        /// Ona
        /// </summary>
        Ona = 4,

        /// <summary>
        /// Tv
        /// </summary>
        Tv = 5
    }

    /// <summary>
    /// Model for anime
    /// </summary>
    public class Anime : Preview
    {
        /// <summary>
        /// Gets or Sets Time
        /// </summary>
        [Required]
        public string Time { get; set; } = null!;

        /// <summary>
        /// Gets or Sets TrailerUrl
        /// </summary>
        public string TrailerUrl { get; set; } = null!;

        /// <summary>
        /// Gets or Sets ScreenshotPath
        /// </summary>
        public List<string> ScreenshotPath { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [Required]
        public AnimeTypeEnum AnimeType { get; set; }

        /// <summary>
        /// Gets or Sets Studio
        /// </summary>
        [Required]
        public string Studio { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Content
        /// </summary>
        public List<Content>? Content { get; set; }

        /// <summary>
        /// Gets or Sets Episodes
        /// </summary>
        public List<Episode>? Episodes { get; set; }
    }
}
