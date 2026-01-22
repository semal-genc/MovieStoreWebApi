using MediatR;
using MovieStore.Application.DTOs;

namespace MovieStore.Application.Queries.Director.GetDirectorById
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