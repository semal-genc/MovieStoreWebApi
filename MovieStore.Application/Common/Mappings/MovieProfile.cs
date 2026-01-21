using AutoMapper;
using MovieStore.Application.Commands.Movie.CreateMovie;
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
        }
    }
}