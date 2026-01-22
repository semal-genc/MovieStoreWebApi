using MediatR;
using MovieStore.Application.DTOs;

namespace MovieStore.Application.Queries.Movie.GetInactiveMovies
{
    public class GetInactiveMoviesQuery : IRequest<List<MovieDto>>
    {
        
    }
}