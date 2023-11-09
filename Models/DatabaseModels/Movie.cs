using System.ComponentModel.DataAnnotations;

namespace Models.DatabaseModels
{
    /// <summary>
    /// Movie entity
    /// </summary>
    public class Movie : BaseType
    {
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal TicketPrice { get; set; }

        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        public List<MovieGenre> Genres { get; set; }

        public Movie()
        {
            Genres = new List<MovieGenre>();
        }
    }
}