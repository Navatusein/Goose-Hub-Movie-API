namespace MovieApi.Models
{
    /// <summary>
    /// Content quality enum
    /// </summary>
    public enum ContentQuality
    {
        /// <summary>
        /// 480p
        /// </summary>
        SD,
        /// <summary>
        /// 720p
        /// </summary>
        HD,
        /// <summary>
        /// 1080p
        /// </summary>
        FullHD
    }

    /// <summary>
    /// Model for episode, movie data
    /// </summary>
    public class Content
    {
        /// <summary>
        /// Gets or Sets Quality
        /// </summary>
        public ContentQuality Quality { get; set; }

        /// <summary>
        /// Gets or Sets Path
        /// </summary>
        public string Path { get; set; } = null!;
    }
}
