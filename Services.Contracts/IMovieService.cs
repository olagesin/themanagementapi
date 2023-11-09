using Models.DTOs;
using SharedUtilities.FilterParameters;

namespace Services.Contracts
{
    public interface IMovieService
    {
        Task<ResponseModel<GetMovieDto>> AddMovie(AddMovieDto model);

        Task<ResponseModel<GetMovieDto>> UpdateMovie(string movieId, UpdateMovie model);

        Task<ResponseModel<GetMovieDto>> GetMovie(string movieId);

        Task<ResponseModel<List<GetMovieDto>>> ListMovies(MovieParameters parameters);

        Task<ResponseModel<bool>> DeleteMovie(string movieId);
    }
}
