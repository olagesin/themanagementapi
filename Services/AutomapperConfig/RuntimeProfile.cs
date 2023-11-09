using AutoMapper;
using Models.DatabaseModels;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AutomapperConfig
{
    internal class RuntimeProfile : Profile
    {
        public RuntimeProfile()
        {
            CreateMap<AddCountryDto, Country>();

            CreateMap<Country, GetCountryDto>();

            CreateMap<UpdateCountryDto, Country>();


            CreateMap<Genre, GetGenreDto>();

            CreateMap<AddGenreDto, Genre>();

            CreateMap<UpdateCountryDto,  Genre>();


            CreateMap<AddMovieDto, Movie>();

            CreateMap<Movie, GetMovieDto>()
                .ForMember(c => c.Country, option => option
                .MapFrom(c => new GetCountryDto()
                {
                    Description = c.Country.Description,
                    IsActive = c.Country.IsActive,
                    Name = c.Country.Name,
                    UUID = c.Country.UUID,
                }))
                .ForMember(c => c.Genres, option => option
                .MapFrom(c => c.Genres.Select(x => new GetGenreDto()
                {
                    Description = x.Genre.Description,
                    Name = x.Genre.Name,
                    UUID = x.UUID,
                    IsActive = x.IsActive,
                }).ToList()));
        }
    }
}
