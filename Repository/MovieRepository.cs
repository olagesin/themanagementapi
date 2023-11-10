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
            .Include(c => c.Genres)
            .ThenInclude(c => c.Genre)
            .Include(c => c.Country)
            .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Movie> GetMovieByUUID(string uuid, bool trackChanges) =>
            await ListAll(trackChanges)
            .Include(c => c.Genres)
            .ThenInclude(c => c.Genre)
            .Include(c => c.Country)
            .FirstOrDefaultAsync(c => c.UUID == uuid);

        public async Task<ResponseModel<List<Movie>>> ListMovies(MovieParameters parameters, bool trackChanges)
        {
            var data = await ListAll(trackChanges)
                .Include(c => c.Genres)
                .ThenInclude(c => c.Genre)
                .Include(c => c.Country)
                .FilterByReleaseDate(parameters.MinDate, parameters.MaxDate)
                .FilterByCountry(parameters.CountryId)
                .FilterByRating(parameters.MaxRating)
                .FilterByPrice(parameters.MinPrice, parameters.MaxPrice)
                .FilteByGenreId(parameters.GenreId)
                .Paginate(parameters.PageNumber, parameters.PageSize)
                .ToListAsync();

            var count = await ListAll(false)
                .FilterByReleaseDate(parameters.MinDate, parameters.MaxDate)
                .FilterByCountry(parameters.CountryId)
                .FilterByRating(parameters.MaxRating)
                .FilterByPrice(parameters.MinPrice, parameters.MaxPrice)
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
