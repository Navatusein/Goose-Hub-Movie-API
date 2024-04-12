using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    /// <summary>
    /// Model for image upload
    /// </summary>
    public class UploadDto
    {
        /// <summary>
        /// Gets or Sets MovieId
        /// </summary>
        [Required]
        public string ContentId { get; set; } = null!;

        /// <summary>
        /// Gets or Sets IsEpisode
        /// </summary>
        [Required]
        public IFormFile File { get; set; } = null!;
    }
}
