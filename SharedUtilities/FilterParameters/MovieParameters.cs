using System.ComponentModel.DataAnnotations;

namespace SharedUtilities.FilterParameters
{
    public class MovieParameters : RequestParameters
    {
        [Range(1, 5)]
        public int MaxRating { get; set; } = 5;
        public decimal MinPrice { get; set; } = decimal.MinValue;
        public decimal MaxPrice { get; set; } = decimal.MaxValue;
        public string? CountryId { get; set; }
        //public List<string> GenreIds { get; set; } = new List<string>();
        public string? GenreId { get; set; }
    }
}
