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
            CreateMap<CreateDirectorCommand, Director>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Movies, opt => opt.Ignore());

            CreateMap<UpdateDirectorCommand, Director>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Movies, opt => opt.Ignore());

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