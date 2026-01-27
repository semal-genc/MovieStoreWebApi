using MediatR;
using MovieStore.Application.Features.Directors.Dtos;

namespace MovieStore.Application.Features.Directors.Queries.GetDirectorById
{
    public class GetDirectorByIdQuery : IRequest<DirectorDetailDto>
    {
        public int Id { get; set; }

        public GetDirectorByIdQuery(int id)
        {
            Id = id;
        }
    }
}