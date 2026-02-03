
using AutoMapper;
using MovieStore.Application.Features.Actors.Commands.CreateActor;
using MovieStore.Application.Features.Actors.Commands.UpdateActor;
using MovieStore.Application.Features.Actors.Dtos;
using MovieStore.Domain.Entities;

namespace MovieStore.Application.Common.Mappings
{
    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
            CreateMap<CreateActorCommand, Actor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Movies, opt => opt.Ignore());

            CreateMap<UpdateActorCommand, Actor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Movies, opt => opt.Ignore());

            CreateMap<Actor, ActorDto>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                );

            CreateMap<Actor, ActorDetailDto>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                );
        }
    }
}