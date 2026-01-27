using MediatR;
using MovieStore.Application.Features.Actors.Dtos;

namespace MovieStore.Application.Features.Actors.Queries.GetActorById
{
    public class GetActorByIdQuery : IRequest<ActorDetailDto>
    {
        public int Id { get; set; }

        public GetActorByIdQuery(int id)
        {
            Id = id;
        }
    }
}
