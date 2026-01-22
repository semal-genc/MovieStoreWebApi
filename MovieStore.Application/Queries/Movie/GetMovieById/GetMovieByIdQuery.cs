using MediatR;
using MovieStore.Application.DTOs;

namespace MovieStore.Application.Queries.Movie.GetMovieById
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