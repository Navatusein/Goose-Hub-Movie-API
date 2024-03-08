using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dto
{
    /// <summary>
    /// Sort param enum
    /// </summary>
    public enum SortParam
    {

    }

    /// <summary>
    /// Model for query request
    /// </summary>
    public class QueryDto
    {
        /// <summary>
        /// Gets or Sets Genres
        /// </summary>
        [Required]
        public List<string> Genres { get; set; } = null!;

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
        /// Gets or Sets Query
        /// </summary>
        [Required]
        public string Query { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Query
        /// </summary>
        [Required]
        public SortParam Sort { get; set; }


        /// <summary>
        /// Gets or Sets ContentType
        /// </summary>
        [Required]
        public ContentTypeEnum ContentType { get; set; }
    }
}
