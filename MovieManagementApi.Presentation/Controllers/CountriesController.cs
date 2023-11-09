using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Services.Contracts;
using SharedUtilities.FilterParameters;

namespace MovieManagementApi.Presentation.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public CountriesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpOptions]
        public IActionResult Get()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT, PATCH");

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(GlobalResponse<GetCountryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(AddCountryDto model)
        {
            var result = await serviceManager.CountryService.AddCountryAsync(model);

            if (result.Status != ResponseStatus.Success)
            {
                ModelState.AddModelError("Failed", result.Message);
                return UnprocessableEntity(ResponseBuilder.BuildResponse<object>(ModelState, null));
            }
            else
            {
                return Created("", ResponseBuilder.BuildResponse(null, result.Data));
            }
        }

        [HttpGet("get-by-id")]
        [ProducesResponseType(typeof(GlobalResponse<GetCountryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetCountry(string countryId)
        {
            var result = await serviceManager.CountryService.GetCountryAsync(countryId);

            if (result.Status == ResponseStatus.NotFound)
            {
                ModelState.AddModelError("Not found.", result.Message);
                return NotFound(ResponseBuilder.BuildResponse<object>(ModelState, null));
            }
            else
            {
                return Ok(ResponseBuilder.BuildResponse(null, result.Data));
            }
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(typeof(GlobalResponse<GetCountryDto[]>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> ListCountries([FromQuery]CountryParameters parameters)
        {
            var result = await serviceManager.CountryService.ListCountriesAsync(parameters);

            return Ok(ResponseBuilder.BuildResponse(null, result.Data));
        }

        [HttpPut]
        [ProducesResponseType(typeof(GlobalResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateMovie(string countryId, UpdateCountryDto model)
        {
            var result = await serviceManager.CountryService.UpdateCountryAsync(countryId, model);

            if (result.Status == ResponseStatus.Failed)
            {
                ModelState.AddModelError("Failed", result.Message);
                return UnprocessableEntity(ResponseBuilder.BuildResponse<object>(ModelState, null));
            }
            else if (result.Status == ResponseStatus.NotFound)
            {
                ModelState.AddModelError("Not found.", result.Message);
                return NotFound(ResponseBuilder.BuildResponse<object>(ModelState, null));
            }
            else
            {
                return Created("", ResponseBuilder.BuildResponse(null, result.Data));
            }
        }
    }
}
