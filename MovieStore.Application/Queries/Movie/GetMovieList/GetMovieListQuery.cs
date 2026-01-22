using MediatR;
using MovieStore.Application.DTOs;

namespace MovieStore.Application.Queries.Movie.GetMovieList
{
    public class GetMovieListQuery : IRequest<List<MovieDto>>
    {
        
    }
}