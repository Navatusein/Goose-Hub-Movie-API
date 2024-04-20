using MovieApi.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    /// <summary>
    /// Sort param enum
    /// </summary>
    public enum SortParam
    {
        /// <summary>
        /// No sort
        /// </summary>
        None,
    }

    /// <summary>
    /// Model for query request
    /// </summary>
    public class QueryDto
    {
        /// <summary>
        /// Gets or Sets Page
        /// </summary>
        [Required]
        [Range(1, Int32.MaxValue)]
        [DefaultValue(1)]
        public int Page { get; set; }

        /// <summary>
        /// Gets or Sets PageSize
        /// </summary>
        [Required]
        [Range(1, Int32.MaxValue)]
        [DefaultValue(20)]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or Sets Genres
        /// </summary>
        public List<string> Genres { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Query
        /// </summary>
        public string? Query { get; set; } = null!;

        /// <summary>
        /// Gets or Sets PageSize
        /// </summary>
        public int? YearStart { get; set; }

        /// <summary>
        /// Gets or Sets PageSize
        /// </summary>
        public int? YearEnd { get; set; }

        /// <summary>
        /// Gets or Sets ContentType
        /// </summary>
        public ContentTypeEnum? ContentType { get; set; }

        /// <summary>
        /// Gets or Sets StatusList
        /// </summary>
        public List<StatusEnum> Statuses { get; set; } = null!;

        /// <summary>
        /// Gets or Sets AnimeTypeList
        /// </summary>
        public List<AnimeTypeEnum> AnimeTypes { get; set; } = null!;

        /// <summary>
        /// Gets or Sets Query
        /// </summary>
        public SortParam Sort { get; set; }
    }
}
