using AutoMapper;
using Models.DatabaseModels;
using Models.DTOs;
using Repositories.Contracts;
using Services.Contracts;
using SharedUtilities.FilterParameters;

namespace Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GenreService(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task<ResponseModel<GetGenreDto>> AddGenreAsync(AddGenreDto model)
        {
            var genreToAdd = _mapper.Map<Genre>(model);

            await _repositoryManager.GenreRepository.CreateGenre(genreToAdd);
            await _repositoryManager.SaveChangesAsync();

            var createdGenre = _repositoryManager.GenreRepository
                .GetGenreByUUID(genreToAdd.UUID, false);

            if (createdGenre is null)
            {
                return new ResponseModel<GetGenreDto>()
                {
                    Message = "Failed to create genre.",
                    Status = ResponseStatus.Failed
                };
            }
            else
            {
                return new ResponseModel<GetGenreDto>()
                {
                    Message = "success.",
                    Status = ResponseStatus.Success,
                    Data = _mapper.Map<GetGenreDto>(createdGenre)
                };
            }
        }

        public Task<ResponseModel<List<GetGenreDto>>> GetAllGenreAsync(GenreParameters parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<GetGenreDto>> GetGenreAsync(string genreId)
        {
            var existingGenre = await _repositoryManager.GenreRepository
                .GetGenreByUUID(genreId, false);

            if(existingGenre is null)
            {
                return new ResponseModel<GetGenreDto>()
                {
                    Message = "Genre not found.",
                    Status = ResponseStatus.NotFound
                };
            }
            else
            {
                return new ResponseModel<GetGenreDto>()
                {
                    Message = "success.",
                    Status = ResponseStatus.Success,
                    Data = _mapper.Map<GetGenreDto>(existingGenre)
                };
            }
        }

        public async Task<ResponseModel<GetGenreDto>> UpdateGenreAsync(string genreId, UpdateGenreDto model)
        {
            var existingGenre = await _repositoryManager.GenreRepository
                .GetGenreByUUID(genreId, false);

            if (existingGenre is null)
            {
                return new ResponseModel<GetGenreDto>()
                {
                    Message = "Genre not found.",
                    Status = ResponseStatus.NotFound
                };
            }
            else
            {
                var genreToUpdate = _mapper.Map(model, existingGenre);
                _repositoryManager.GenreRepository.UpdateGenre(genreToUpdate);
                await _repositoryManager.SaveChangesAsync();

                var updatedGenre = await _repositoryManager.GenreRepository
                    .GetGenreByUUID(genreId, false);

                return new ResponseModel<GetGenreDto>()
                {
                    Data = _mapper.Map<GetGenreDto>(updatedGenre),
                    Message = "success.",
                    Status = ResponseStatus.Failed
                };
            }
        }
    }
}
