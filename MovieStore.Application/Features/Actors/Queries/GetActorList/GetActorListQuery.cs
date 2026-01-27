using MediatR;
using MovieStore.Application.Features.Actors.Dtos;

namespace MovieStore.Application.Features.Actors.Queries.GetActorList
{
    public class GetActorListQuery : IRequest<List<ActorDto>>
    {

    }
}
