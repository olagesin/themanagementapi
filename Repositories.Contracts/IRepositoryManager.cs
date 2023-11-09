namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        ICountryRepository CountryRepository { get; }
        IGenreRepository GenreRepository { get; }
        IMovieGenreRepository MovieGenreRepository { get; }
        IMovieRepository MovieRepository { get; }
        Task SaveChangesAsync();
    }
}
