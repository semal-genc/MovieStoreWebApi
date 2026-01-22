using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.DTOs;
using MovieStore.Application.Interfaces;
using MovieStore.Application.Queries.Movie.GetMovieList;

namespace MovieStore.Application.Queries.Movie
{
    public class GetMovieListQueryHandler : IRequestHandler<GetMovieListQuery, List<MovieDto>>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMovieListQueryHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MovieDto>> Handle(GetMovieListQuery request, CancellationToken cancellationToken)
        {
            var movies = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return movies;
        }
    }
}