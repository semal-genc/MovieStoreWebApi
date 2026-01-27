using AutoMapper;
using MovieStore.Application.Features.Movies.Commands.CreateMovie;
using MovieStore.Application.Features.Movies.Commands.UpdateMovie;
using MovieStore.Application.Features.Movies.Dtos;
using MovieStore.Domain.Entities;

namespace MovieStore.Application.Common.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<CreateMovieCommand, Movie>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.DirectorFullName, opt => opt.MapFrom(src => $"{src.Director.FirstName} {src.Director.LastName}"));

            CreateMap<UpdateMovieCommand, Movie>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());
        }
    }
}