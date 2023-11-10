using Models.DatabaseModels;

namespace Repositories.QueryExtensions
{
    public static class MovieExtensions
    {

        public static IQueryable<Movie> FilteByGenreIds(this IQueryable<Movie> data, List<string> genreIds)
        {
            if(genreIds is null || genreIds.Count == 0)
                return data;

            return data.Where(c => genreIds.Any(x => c.Genres.Any(c => c.UUID == x)));
        }

        public static IQueryable<Movie> FilteByGenreId(this IQueryable<Movie> data, string genreId)
        {
            if(string.IsNullOrWhiteSpace(genreId))
                return data;

            return data.Where(c => c.Genres.Any(x => x.Genre.UUID == genreId));
        }

        public static IQueryable<Movie> FilterByPrice(this IQueryable<Movie> data, decimal minPrice, decimal maxPrice) =>
            data.Where(c => c.TicketPrice >= minPrice && c.TicketPrice <= maxPrice);

        public static IQueryable<Movie> FilterByRating(this IQueryable<Movie> data, int maxRating) =>
           data.Where(c => c.Rating <= maxRating);

        public static IQueryable<Movie> FilterByCountry(this IQueryable<Movie> data, string countryId)
        {
            if (string.IsNullOrWhiteSpace(countryId))
                return data;

            return data.Where(c => c.Country.UUID == countryId);
        }

        public static IQueryable<Movie> FilterByReleaseDate(this IQueryable<Movie> data, DateTime minDate, DateTime maxDate) =>
            data.Where(c => c.ReleaseDate.Date >= minDate.Date && c.ReleaseDate.Date <= maxDate.Date);
    }
}
