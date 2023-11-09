using Models.DatabaseModels;
using Models.DTOs;
using SharedUtilities.FilterParameters;

namespace Repositories.Contracts
{
    public interface IMovieGenreRepository
    {
        Task AddMovieGenre(MovieGenre movieGenre);

        Task AddMovieMultipleGenres(List<MovieGenre> movieGenres);

        void UpdateMovieGenre(MovieGenre model);

        void DeleteMovieGenre(MovieGenre movieGenre);

        Task<MovieGenre> GetMovieGenre(int movieId, int genreId, bool trackChanges);

        Task<ResponseModel<List<MovieGenre>>> ListMovieGenres(MovieGenreParameters parameters, bool trackChanges);
    }
}
