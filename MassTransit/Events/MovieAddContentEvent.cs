using MassTransit;
using MovieApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.MassTransit.Events
{
    /// <summary>
    /// 
    /// </summary>
    [EntityName("movie-api-movie-add-content")]
    public class MovieAddContentEvent
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string MovieId { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Content Content { get; set; } = null!;
    }
}
