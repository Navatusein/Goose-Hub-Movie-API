using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace MovieApi.Dto
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
    public class AnimeDto : PreviewDto
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
        /// Gets or Sets Screenshots
        /// </summary>
        public List<string> Screenshots { get; set; } = null!;

        /// <summary>
        /// Gets or Sets ScreenshotIds
        /// </summary>
        public List<string> ScreenshotIds { get; set; } = null!;

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
        /// Gets or Sets MovieUrl
        /// </summary>
        public string MovieUrl { get; set; } = null!;

        /// <summary>
        /// Gets or Sets MovieId
        /// </summary>
        public string MovieId { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Episodes
        /// </summary>
        public List<EpisodeDto> Episodes { get; set; } = null!;
    }
}
