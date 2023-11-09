using Models.DTOs;
using SharedUtilities.FilterParameters;

namespace Services.Contracts
{
    public interface IGenreService
    {
        Task<ResponseModel<GetGenreDto>> GetGenreAsync(string genreId);

        Task<ResponseModel<GetGenreDto>> AddGenreAsync(AddGenreDto model);

        Task<ResponseModel<GetGenreDto>> UpdateGenreAsync(string genreId, UpdateGenreDto model);

        Task<ResponseModel<List<GetGenreDto>>> GetAllGenreAsync(GenreParameters parameters);
    }
}