using Microsoft.EntityFrameworkCore;
using Models.DatabaseModels;
using Models.DTOs;
using Repositories.Contracts;
using Repositories.QueryExtensions;
using SharedUtilities;
using SharedUtilities.FilterParameters;

namespace Repositories
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task CreateMovie(Movie movie) => await CreateAsync(movie);

        public void DeleteMovie(Movie movie) => Delete(movie);

        public async Task<Movie> GetMovieById(int id, bool trackChanges) =>
            await ListAll(trackChanges)
            .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Movie> GetMovieByUUID(string uuid, bool trackChanges) =>
            await ListAll(trackChanges)
            .FirstOrDefaultAsync(c => c.UUID == uuid);

        public async Task<ResponseModel<List<Movie>>> ListMovies(MovieParameters parameters, bool trackChanges)
        {
            var data = await ListAll(trackChanges)
                .Paginate(parameters.PageNumber, parameters.PageSize)
                .ToListAsync();

            var count = await ListAll(false)
                .CountAsync();

            var pagedList = new PagedList<Movie>(data, count,
                parameters.PageNumber, parameters.PageSize);


            return new ResponseModel<List<Movie>>()
            {
                Message = "success",
                Status = ResponseStatus.Success,
                Data = pagedList,
                MetaData = pagedList.MetaData
            };
        }

        public void UpdateMovie(Movie movie) => Update(movie);
    }
}
