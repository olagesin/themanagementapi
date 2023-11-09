using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class GetMovieDto
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Rating { get; set; }
        public decimal TicketPrice { get; set; }
        public GetCountryDto Country { get; set; }
        public List<GetGenreDto> Genres { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class UpdateMovie
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Range(0, 5)]
        public int Rating { get; set; }
        [Range(0, 100000)]
        public decimal TicketPrice { get; set; }
        public string CountryId { get; set; }
    }

    public class UpdateMovieGenre
    {
        public string MovieId { get; set; }
        public List<string> GenreIds { get; set; }
    }

    public class AddMovieDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal TicketPrice { get; set; }

        [Required]
        public string CountryShotInId { get; set; }

        [Required]
        public List<string> GenreIds { get; set; }

        [Required]
        public string PhotoUrl { get; set; }
    }


    public class AddGenreDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class GetGenreDto
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateGenreDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public ResponseStatus Status { get; set; }
        public MetaData MetaData { get; set; }
    }

    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }

    public enum ResponseStatus
    {
        Success,
        Failed,
        NotFound,
        NoAccess,
        NoContent
    }
}
