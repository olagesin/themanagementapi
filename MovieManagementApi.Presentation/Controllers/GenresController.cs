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
    public class GenresController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public GenresController(IServiceManager serviceManager)
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
        [ProducesResponseType(typeof(GlobalResponse<GetGenreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(AddGenreDto model)
        {
            var result = await serviceManager.GenreService.AddGenreAsync(model);

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
        [ProducesResponseType(typeof(GlobalResponse<GetGenreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetGenre(string genreId)
        {
            var result = await serviceManager.GenreService.GetGenreAsync(genreId);

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
        [ProducesResponseType(typeof(GlobalResponse<GetGenreDto[]>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> ListGenres([FromQuery]GenreParameters parameters)
        {
            var result = await serviceManager.GenreService.GetAllGenreAsync(parameters);

            return Ok(ResponseBuilder.BuildResponse(null, result.Data));
        }

        [HttpPut]
        [ProducesResponseType(typeof(GlobalResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateMovie(string genreId, UpdateGenreDto model)
        {
            var result = await serviceManager.GenreService.UpdateGenreAsync(genreId, model);

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
