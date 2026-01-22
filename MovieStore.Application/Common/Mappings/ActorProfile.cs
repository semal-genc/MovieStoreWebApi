
using AutoMapper;
using MovieStore.Application.Commands.Actor.CreateActor;
using MovieStore.Application.Commands.Actor.UpdateActor;
using MovieStore.Application.DTOs;
using MovieStore.Domain.Entities;

namespace MovieStore.Application.Common.Mappings
{
    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
            CreateMap<CreateActorCommand, Actor>();

            CreateMap<UpdateActorCommand, Actor>();

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