using MediatR;
using MovieStore.Application.DTOs;

namespace MovieStore.Application.Queries.Actor.GetActorById
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
