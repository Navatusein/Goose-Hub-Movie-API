using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MovieApi.Dtos
{
    /// <summary>
    /// Model for pagination data
    /// </summary>
    public class PaginationDto
    {
        /// <summary>
        /// Gets or Sets Page
        /// </summary>
        [Required]
        public int Page { get; set; }

        /// <summary>
        /// Gets or Sets PageSize
        /// </summary>
        [Required]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or Sets PageSize
        /// </summary>
        [Required]
        public long TotalCount { get; set; }

        /// <summary>
        /// Gets or Sets PageSize
        /// </summary>
        [Required]
        public int ReturnedCount { get; set; }

        /// <summary>
        /// Gets or Sets Data
        /// </summary>
        [Required]
        public List<PreviewDto> Data { get; set; } = null!;
    }
}
