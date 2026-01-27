using MediatR;
using MovieStore.Application.Features.Movies.Dtos;

namespace MovieStore.Application.Features.Movies.Queries.GetMovieList
{
    public class GetMovieListQuery : IRequest<List<MovieDto>>
    {
        
    }
}