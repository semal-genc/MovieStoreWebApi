using MediatR;
using MovieStore.Application.Features.Movies.Dtos;

namespace MovieStore.Application.Features.Movies.Queries.GetMovieById
{
    public class GetMovieByIdQuery : IRequest<MovieDto>
    {
        public int Id { get; set; }

        public GetMovieByIdQuery(int id)
        {
            Id = id;
        }
    }
}