using Microsoft.EntityFrameworkCore;
using Models.DatabaseModels;
using Models.DTOs;
using Repositories.Contracts;
using Repositories.QueryExtensions;
using SharedUtilities;
using SharedUtilities.FilterParameters;

namespace Repositories
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task CreateCountry(Country model) => await CreateAsync(model);

        public void DeleteCountry(Country country) => Delete(country);

        public async Task<Country> GetCountryById(int countryId, bool trackChanges) => 
            await ListAll(trackChanges)
            .FirstOrDefaultAsync(c => c.Id == countryId);

        public  async Task<Country> GetCountryByUUID(string countryUuid, bool trackChanges) =>
            await ListAll(trackChanges)
            .FirstOrDefaultAsync(c => c.UUID == countryUuid);

        public async Task<ResponseModel<List<Country>>> ListCountries(CountryParameters parameters, bool trackChanges)
        {
            var data = await ListAll(trackChanges)
                .Paginate(parameters.PageNumber, parameters.PageSize)
                .ToListAsync();

            var count = await ListAll(false)
                .CountAsync();

            var pagedList = new PagedList<Country>(data, count,
                parameters.PageNumber, parameters.PageSize);


                return new ResponseModel<List<Country>>()
                {
                    Message = "success",
                    Status = ResponseStatus.Success,
                    Data = pagedList,
                    MetaData = pagedList.MetaData
                };
        }

        public void UpdateCountry(Country model) => Update(model);
    }
}
