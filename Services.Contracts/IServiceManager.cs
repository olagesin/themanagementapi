namespace Services.Contracts
{
    public interface IServiceManager
    {
        IMovieService MovieService { get; }
        ICountryService CountryService { get; }
        IGenreService GenreService { get; }
    }
}
