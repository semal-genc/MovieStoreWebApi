using AutoMapper;
using MovieStore.Application.Commands.Director.CreateDirector;
using MovieStore.Application.Commands.Director.UpdateDirector;
using MovieStore.Application.DTOs;
using MovieStore.Domain.Entities;

namespace MovieStore.Application.Common.Mappings
{
    public class DirectorProfile : Profile
    {
        public DirectorProfile()
        {
            CreateMap<CreateDirectorCommand, Director>();

            CreateMap<UpdateDirectorCommand, Director>();

            CreateMap<Director, DirectorDto>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                );

            CreateMap<Director, DirectorDetailDto>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                );
        }
    }
}