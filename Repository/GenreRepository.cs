using Microsoft.EntityFrameworkCore;
using Models.DatabaseModels;
using Models.DTOs;
using Repositories.Contracts;
using Repositories.QueryExtensions;
using SharedUtilities;
using SharedUtilities.FilterParameters;

namespace Repositories
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task CreateGenre(Genre genre) => await CreateAsync(genre);

        public void DeleteGenre(Genre genre) => Delete(genre);

        //public async Task<bool> CheckIfGenresExist(List<string> genreIds, bool trackchanges) =>
        //    await ListAll(trackchanges)
        //    .l

        public async Task<Genre> GetGenreById(int id, bool trackChanges) =>
            await ListAll(trackChanges)
            .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Genre> GetGenreByUUID(string uuid, bool trackChanges) =>
             await ListAll(trackChanges)
            .FirstOrDefaultAsync(c => c.UUID == uuid);

        public async Task<ResponseModel<List<Genre>>> ListGenres(GenreParameters parameters, bool trackChanges)
        {
            var data = await ListAll(trackChanges)
            .Paginate(parameters.PageNumber, parameters.PageSize)
            .ToListAsync();

            var count = await ListAll(false)
                .CountAsync();

            var pagedList = new PagedList<Genre>(data, count,
                parameters.PageNumber, parameters.PageSize);


            return new ResponseModel<List<Genre>>()
            {
                Message = "success",
                Status = ResponseStatus.Success,
                Data = pagedList,
                MetaData = pagedList.MetaData
            };
        }

        public void UpdateGenre(Genre genre) => Update(genre);
    }
}
