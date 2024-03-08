using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieApi.Dto
{
    /// <summary>
    /// Model for errors
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [Required]
        public string Id { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        [Required]
        public string Message { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        [Required]
        public string Code { get; set; } = null!;
    }
}
