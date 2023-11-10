using AutoMapper;
using Models.DatabaseModels;
using Models.DTOs;
using Repositories.Contracts;
using Services.Contracts;
using SharedUtilities.FilterParameters;

namespace Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CountryService(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task<ResponseModel<GetCountryDto>> AddCountryAsync(AddCountryDto model)
        {
            var countryToAdd = _mapper.Map<Country>(model);

            var existingCountry = await _repositoryManager.CountryRepository
                .GetByName(countryToAdd.Name, false);

            if(existingCountry is not null)
            {
                return new ResponseModel<GetCountryDto>()
                {
                    Message = "Country has already exists.",
                    Status = ResponseStatus.Failed
                };
            }

            await _repositoryManager.CountryRepository.CreateCountry(countryToAdd);
            await _repositoryManager.SaveChangesAsync();

            var createdCountry = await _repositoryManager.CountryRepository
                .GetCountryByUUID(countryToAdd.UUID, false);

            if(createdCountry is null)
            {
                return new ResponseModel<GetCountryDto>()
                {
                    Message = "Failed to create country.",
                    Status = ResponseStatus.Failed
                };
            }
            else
            {
                return new ResponseModel<GetCountryDto>()
                {
                    Message = "success.",
                    Status = ResponseStatus.Success,
                    Data = _mapper.Map<GetCountryDto>(createdCountry)
                };
            }
        }

        public async Task<ResponseModel<GetCountryDto>> GetCountryAsync(string countryId)
        {
            var country = await _repositoryManager.CountryRepository
                .GetCountryByUUID(countryId, false);


            if (country is null)
            {
                return new ResponseModel<GetCountryDto>()
                {
                    Message = "Failed to create country.",
                    Status = ResponseStatus.Failed
                };
            }
            else
            {
                return new ResponseModel<GetCountryDto>()
                {
                    Message = "success.",
                    Status = ResponseStatus.Success,
                    Data = _mapper.Map<GetCountryDto>(country)
                };
            }
        }

        public async Task<ResponseModel<List<GetCountryDto>>> ListCountriesAsync(CountryParameters parameters)
        {
            var listCountriesResult = await _repositoryManager.CountryRepository
                .ListCountries(parameters, false);

            return new ResponseModel<List<GetCountryDto>>()
            {
                Message = "success.",
                Status = listCountriesResult.Status,
                Data = _mapper.Map<List<GetCountryDto>>(listCountriesResult.Data),
                MetaData = listCountriesResult.MetaData
            };
        }

        public async Task<ResponseModel<GetCountryDto>> UpdateCountryAsync(string countryId, UpdateCountryDto model)
        {
            var existingCountry = await _repositoryManager.CountryRepository
                .GetCountryByUUID(countryId, true);

            if(existingCountry is null)
            {
                return new ResponseModel<GetCountryDto>()
                {
                    Message = "country not found.",
                    Status = ResponseStatus.NotFound
                };
            }
            else
            {
                var countryToUpdate = _mapper.Map(model, existingCountry);
                _repositoryManager.CountryRepository.UpdateCountry(countryToUpdate);
                await _repositoryManager.SaveChangesAsync();

                var updatedCountry = await _repositoryManager.CountryRepository
                    .GetCountryByUUID(countryId, false);

                return new ResponseModel<GetCountryDto>()
                {
                    Message = "success.",
                    Status = ResponseStatus.Success,
                    Data = _mapper.Map<GetCountryDto>(updatedCountry)
                };
            }
        }
    }
}
