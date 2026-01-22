using MediatR;
using MovieStore.Application.DTOs;

namespace MovieStore.Application.Queries.Actor.GetActorList
{
    public class GetActorListQuery : IRequest<List<ActorDto>>
    {

    }
}
