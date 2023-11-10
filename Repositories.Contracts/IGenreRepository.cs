using Models.DatabaseModels;
using Models.DTOs;
using SharedUtilities.FilterParameters;

namespace Repositories.Contracts
{
    public interface IGenreRepository
    {
        Task CreateGenre(Genre genre);

        void UpdateGenre(Genre genre);

        void DeleteGenre(Genre genre);

        Task<Genre> GetGenreById(int id, bool trackChanges);

        Task<Genre> GetGenreByUUID(string uuid, bool trackChanges);

        Task<Genre> GetByName(string name, bool trackChanges);

        Task<ResponseModel<List<Genre>>> ListGenres(GenreParameters parameters, bool trackChanges);
    }
}
