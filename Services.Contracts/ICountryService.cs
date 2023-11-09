using Models.DTOs;
using SharedUtilities.FilterParameters;

namespace Services.Contracts
{
    public interface ICountryService
    {
        Task<ResponseModel<GetCountryDto>> GetCountryAsync(string countryId);

        Task<ResponseModel<GetCountryDto>> AddCountryAsync(AddCountryDto model);

        Task<ResponseModel<GetCountryDto>> UpdateCountryAsync(string countryId, UpdateCountryDto model);

        Task<ResponseModel<List<GetCountryDto>>> ListCountriesAsync(CountryParameters parameters);
    }
}