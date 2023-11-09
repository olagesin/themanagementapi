using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ICountryRepository> _countryRepository;
        private readonly Lazy<IMovieGenreRepository> _movieGenreRepository;
        private readonly Lazy<IMovieRepository> _movieRepository;
        private readonly Lazy<IGenreRepository> _genreRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _genreRepository = new Lazy<IGenreRepository>(() => new GenreRepository(repositoryContext));
            _countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(repositoryContext));
            _movieGenreRepository = new Lazy<IMovieGenreRepository>(() => new MovieGenreRepository(repositoryContext));
            _movieRepository = new Lazy<IMovieRepository>(() =>  new MovieRepository(repositoryContext));
        }

        public IMovieGenreRepository MovieGenreRepository => _movieGenreRepository.Value;
        public ICountryRepository CountryRepository => _countryRepository.Value;
        public IGenreRepository GenreRepository => _genreRepository.Value;
        public IMovieRepository MovieRepository => _movieRepository.Value;

        public Task SaveChangesAsync() => _repositoryContext.SaveChangesAsync();
    }
}