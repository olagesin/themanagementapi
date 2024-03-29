﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Services.Contracts;
using SharedUtilities.FilterParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieManagementApi.Presentation.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public MoviesController(IServiceManager serviceManager)
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
        [ProducesResponseType(typeof(GlobalResponse<GetMovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(AddMovieDto model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ResponseBuilder.BuildResponse<object>(ModelState, null));
            }

            var result = await serviceManager.MovieService.AddMovie(model);

            if(result.Status != ResponseStatus.Success)
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
        [ProducesResponseType(typeof(GlobalResponse<GetMovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetMovie(string movieId)
        {
            var result = await serviceManager.MovieService.GetMovie(movieId);

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
        [ProducesResponseType(typeof(GlobalResponse<GetMovieDto[]>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> ListMovies([FromQuery]MovieParameters parameters)
        {
            var result = await serviceManager.MovieService.ListMovies(parameters);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.MetaData));

            return Ok(ResponseBuilder.BuildResponse(null, result.Data));
        }


        [HttpDelete]
        [ProducesResponseType(typeof(GlobalResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> DeleteMovie(string movieId)
        {
            var result = await serviceManager.MovieService.DeleteMovie(movieId);

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

        [HttpPut]
        [ProducesResponseType(typeof(GlobalResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateMovie(string movieId, UpdateMovie model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ResponseBuilder.BuildResponse<object>(ModelState, null));
            }

            var result = await serviceManager.MovieService.UpdateMovie(movieId, model);

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

        [HttpPatch("add-genres-to-movie")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AddGenresToMovie(UpdateMovieGenre model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ResponseBuilder.BuildResponse<object>(ModelState, null));
            }

            var result = await serviceManager.MovieService.AddGenresToMovie(model);

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

        [HttpPatch("remove-genres-from-movie")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> RemoveGenresFromMovie(UpdateMovieGenre model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ResponseBuilder.BuildResponse<object>(ModelState, null));
            }

            var result = await serviceManager.MovieService.RemoveGenresFromMovie(model);

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
    }
}
