namespace MovieApi.Dtos
{
    /// <summary>
    /// Model for store min and max year of release
    /// </summary>
    public class YearsInfoDto
    {
        /// <summary>
        /// Gets or Sets MinYear
        /// </summary>
        public int MinYear { get; set; }

        /// <summary>
        /// Gets or Sets MaxYear
        /// </summary>
        public int MaxYear { get; set; }
    }
}
