using AutoMapper;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMovieService> _movieService;
        private readonly Lazy<IGenreService> _genreService;
        private readonly Lazy<ICountryService> _countryService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _movieService = new Lazy<IMovieService>(() => new MovieService(repositoryManager, mapper));
            _genreService = new Lazy<IGenreService>(() => new GenreService(mapper, repositoryManager));
            _countryService = new Lazy<ICountryService>(() => new CountryService(mapper, repositoryManager));
        }

        public IMovieService MovieService => _movieService.Value;
        public IGenreService GenreService => _genreService.Value;
        public ICountryService CountryService => _countryService.Value;
    }
}
