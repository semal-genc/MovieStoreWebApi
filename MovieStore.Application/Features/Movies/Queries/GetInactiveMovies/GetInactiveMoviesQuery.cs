using MediatR;
using MovieStore.Application.Features.Movies.Dtos;

namespace MovieStore.Application.Features.Movies.Queries.GetInactiveMovies
{
    public class GetInactiveMoviesQuery : IRequest<List<MovieDto>>
    {
        
    }
}