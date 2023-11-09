using Models.DatabaseModels;
using Models.DTOs;
using SharedUtilities.FilterParameters;

namespace Repositories.Contracts
{
    public interface ICountryRepository
    {
        Task CreateCountry(Country model);

        void UpdateCountry(Country model);

        void DeleteCountry(Country country);

        Task<Country> GetCountryById(int countryId, bool trackChanges);

        Task<Country> GetCountryByUUID(string countryUuid, bool trackChanges);

        Task<ResponseModel<List<Country>>> ListCountries(CountryParameters parameters, bool trackChanges);
    }
}
