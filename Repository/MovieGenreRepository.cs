using Microsoft.EntityFrameworkCore;
using Models.DatabaseModels;
using Models.DTOs;
using Repositories.Contracts;
using Repositories.QueryExtensions;
using SharedUtilities;
using SharedUtilities.FilterParameters;

namespace Repositories
{
    public class MovieGenreRepository : RepositoryBase<MovieGenre>, IMovieGenreRepository
    {
        public MovieGenreRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task AddMovieGenre(MovieGenre movieGenre) => await CreateAsync(movieGenre);

        public async Task AddMovieMultipleGenres(List<MovieGenre> movieGenres) => await CreateMultipleAsync(movieGenres);

        public void DeleteMovieGenre(MovieGenre movieGenre) => Delete(movieGenre);

        public async Task<MovieGenre> GetMovieGenre(int movieId, int genreId, bool trackChanges) =>
            await ListAll(trackChanges)
            .FirstOrDefaultAsync(c => c.MovieId == movieId && c.GenreId == genreId);

        public async Task<ResponseModel<List<MovieGenre>>> ListMovieGenres(MovieGenreParameters parameters, bool trackChanges)
        {
            var data = await ListAll(trackChanges)
            .Paginate(parameters.PageNumber, parameters.PageSize)
            .ToListAsync();

            var count = await ListAll(false)
                .CountAsync();

            var pagedList = new PagedList<MovieGenre>(data, count,
                parameters.PageNumber, parameters.PageSize);


            return new ResponseModel<List<MovieGenre>>()
            {
                Message = "success",
                Status = ResponseStatus.Success,
                Data = pagedList,
                MetaData = pagedList.MetaData
            };
        }

        public void UpdateMovieGenre(MovieGenre model) => Update(model);
    }
}
