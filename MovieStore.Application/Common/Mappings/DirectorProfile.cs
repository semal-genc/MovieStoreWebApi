using AutoMapper;
using MovieStore.Application.Features.Directors.Commands.CreateDirector;
using MovieStore.Application.Features.Directors.Commands.UpdateDirector;
using MovieStore.Application.Features.Directors.Dtos;
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