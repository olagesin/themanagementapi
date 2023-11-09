using System.ComponentModel.DataAnnotations;

namespace Models.DatabaseModels
{
    public class MovieGenre : BaseEntity
    {
        [Required]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
