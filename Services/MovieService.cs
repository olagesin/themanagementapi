using AutoMapper;
using Models.DatabaseModels;
using Models.DTOs;
using Repositories.Contracts;
using Services.Contracts;
using SharedUtilities.FilterParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{

    public class MovieService : IMovieService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;


        public MovieService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ResponseModel<GetMovieDto>> AddMovie(AddMovieDto model)
        {
            var country = await _repositoryManager.CountryRepository
                .GetCountryByUUID(model.CountryShotInId, true);

            if(country == null)
            {
                return new ResponseModel<GetMovieDto>()
                {
                    Message = "Country not found.",
                    Status = ResponseStatus.Failed
                };
            }
            else
            {
                var movieToCreate = _mapper.Map<Movie>(model);
                movieToCreate.CountryId = country.Id;
                var movieGenresToCreate = new List<MovieGenre>();

                //optimize this later
                foreach (var genreId in model.GenreIds)
                {
                    var genre = await _repositoryManager.GenreRepository
                        .GetGenreByUUID(genreId, false);

                    //one of the genre ids passed is not valid
                    if(genre is null)
                    {
                        return new ResponseModel<GetMovieDto>()
                        {
                            Message = $"genreid {genreId} is invalid.",
                            Status = ResponseStatus.Failed
                        };
                    }

                    movieGenresToCreate.Add(new MovieGenre()
                    {
                        GenreId = genre.Id,
                    });
                }

                movieToCreate.Genres = movieGenresToCreate;


                await _repositoryManager.MovieRepository.CreateMovie(movieToCreate);
                await _repositoryManager.SaveChangesAsync();

                var createdMovie = await _repositoryManager.MovieRepository
                    .GetMovieByUUID(movieToCreate.UUID, false);

                if(createdMovie is null)
                {
                    return new ResponseModel<GetMovieDto>()
                    {
                        Message = "Error creating movie,",
                        Status = ResponseStatus.Failed
                    };
                }
                else
                {
                    return new ResponseModel<GetMovieDto>()
                    {
                        Message = "success.",
                        Status = ResponseStatus.Success,
                        Data = _mapper.Map<GetMovieDto>(createdMovie)
                    };
                }
            }
        }

        public async Task<ResponseModel<bool>> DeleteMovie(string movieId)
        {
            var movie = await _repositoryManager.MovieRepository
                .GetMovieByUUID(movieId, false);

            if (movie is null)
            {
                return new ResponseModel<bool>()
                {
                    Message = "Movie not found.",
                    Status = ResponseStatus.NotFound
                };
            }
            else
            {
                _repositoryManager.MovieRepository.DeleteMovie(movie);
                await _repositoryManager.SaveChangesAsync();

                return new ResponseModel<bool>()
                {
                    Message = "success.",
                    Status = ResponseStatus.Success,
                    Data = true
                };
            }
        }

        public async Task<ResponseModel<GetMovieDto>> GetMovie(string movieId)
        {
            var movie = await _repositoryManager.MovieRepository
                .GetMovieByUUID(movieId, false);

            if(movie is null)
            {
                return new ResponseModel<GetMovieDto>()
                {
                    Message = "Movie not found.",
                    Status = ResponseStatus.NotFound
                };
            }
            else
            {
                return new ResponseModel<GetMovieDto>()
                {
                    Message = "success.",
                    Status = ResponseStatus.Success,
                    Data = _mapper.Map<GetMovieDto>(movie)
                };
            }
        }

        public async Task<ResponseModel<GetMovieDto>> RemoveGenresFromMovie(UpdateMovieGenre model)
        {
            var movie = await _repositoryManager.MovieRepository
                .GetMovieByUUID(model.MovieId, false);

            if (movie is null)
            {
                return new ResponseModel<GetMovieDto>()
                {
                    Message = "Movie not found.",
                    Status = ResponseStatus.NotFound
                };
            }
            else
            {
                var movieGenresToRemove = new List<MovieGenre>();

                foreach (var genreId in model.GenreIds)
                {
                    var genre = await _repositoryManager.GenreRepository
                        .GetGenreByUUID(genreId, false);

                    if (genre is null)
                    {
                        return new ResponseModel<GetMovieDto>()
                        {
                            Message = $"genreId {genreId} is invalid.",
                            Status = ResponseStatus.Failed
                        };
                    }

                    var movieGenre = await _repositoryManager.MovieGenreRepository
                        .GetMovieGenre(movie.Id, genre.Id, false);


                    if (movieGenre is null)
                    {
                        return new ResponseModel<GetMovieDto>()
                        {
                            Message = $"Genre {genre.UUID} does not belong to this movie.",
                            Status = ResponseStatus.Failed
                        };
                    }

                    movieGenresToRemove.Add(movieGenre);
                }

                _repositoryManager.MovieGenreRepository.RemoveMovieMultipleGenres(movieGenresToRemove);
                await _repositoryManager.SaveChangesAsync();

                var updatedMovie = await _repositoryManager.MovieRepository
                    .GetMovieById(movie.Id, false);

                return new ResponseModel<GetMovieDto>()
                {
                    Message = "success.",
                    Status = ResponseStatus.Success,
                    Data = _mapper.Map<GetMovieDto>(updatedMovie)
                };
            }
        }

        public async Task<ResponseModel<GetMovieDto>> AddGenresToMovie(UpdateMovieGenre model)
        {
            var movie = await _repositoryManager.MovieRepository
                .GetMovieByUUID(model.MovieId, true);

            if (movie is null)
            {
                return new ResponseModel<GetMovieDto>()
                {
                    Message = "Movie not found.",
                    Status = ResponseStatus.NotFound
                };
            }
            else
            {
                var movieGenresToAdd = new List<MovieGenre>();

                foreach(var genreId in model.GenreIds)
                {
                    var genre = await _repositoryManager.GenreRepository
                        .GetGenreByUUID(genreId, false);

                    if(genre is null)
                    {
                        return new ResponseModel<GetMovieDto>()
                        {
                            Message = $"genreId {genreId} is invalid.",
                            Status = ResponseStatus.Failed
                        };
                    }

                    var movieGenre = await _repositoryManager.MovieGenreRepository
                        .GetMovieGenre(movie.Id, genre.Id, false);


                    if(movieGenre is not null)
                    {
                        continue; //skip it and don't add it to the list of things to be added
                    }

                    movieGenresToAdd.Add(new MovieGenre()
                    {
                        MovieId = movie.Id,
                        GenreId = genre.Id
                    });
                }


                await _repositoryManager.MovieGenreRepository.AddMovieMultipleGenres(movieGenresToAdd);
                await _repositoryManager.SaveChangesAsync();

                var updatedMovie = await _repositoryManager.MovieRepository
                    .GetMovieById(movie.Id, false);

                return new ResponseModel<GetMovieDto>()
                {
                    Message = "success.",
                    Status = ResponseStatus.Success,
                    Data = _mapper.Map<GetMovieDto>(updatedMovie)
                };
            }
        }

        public async Task<ResponseModel<List<GetMovieDto>>> ListMovies(MovieParameters parameters)
        {
            var listMovies = await _repositoryManager.MovieRepository
                .ListMovies(parameters, false);

            return new ResponseModel<List<GetMovieDto>>()
            {
                Message = "success.",
                Status = listMovies.Status,
                Data = _mapper.Map<List<GetMovieDto>>(listMovies.Data),
                MetaData = listMovies.MetaData
            };
        }

        public async Task<ResponseModel<GetMovieDto>> UpdateMovie(string movieId, UpdateMovie model)
        {
            var movie = await _repositoryManager.MovieRepository
                .GetMovieByUUID(movieId, true);

            if (movie is null)
            {
                return new ResponseModel<GetMovieDto>()
                {
                    Message = "Movie not found.",
                    Status = ResponseStatus.NotFound
                };
            }
            else
            {
                var dataToUpdate = _mapper.Map(model, movie);
                dataToUpdate.LastModifiedDate = DateTime.UtcNow;

                _repositoryManager.MovieRepository.UpdateMovie(dataToUpdate);
                await _repositoryManager.SaveChangesAsync();

                var updatedMovie = await _repositoryManager.MovieRepository
                    .GetMovieByUUID(movieId, false);

                return new ResponseModel<GetMovieDto>()
                {
                    Message = "success.",
                    Data = _mapper.Map<GetMovieDto>(updatedMovie),
                    Status = ResponseStatus.Success
                };
            }
        }
    }
}
