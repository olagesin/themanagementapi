using Models.DatabaseModels;
using Models.DTOs;
using SharedUtilities.FilterParameters;

namespace Repositories.Contracts
{
    public interface IMovieRepository
    {
        Task CreateMovie(Movie movie);

        void UpdateMovie(Movie movie);

        void DeleteMovie(Movie movie);

        Task<Movie> GetMovieById(int id, bool trackChanges);

        Task<Movie> GetMovieByUUID(string uuid, bool trackChanges);

        Task<ResponseModel<List<Movie>>> ListMovies(MovieParameters parameters, bool trackChanges);
    }
}
