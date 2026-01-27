using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Movies.Dtos;
using MovieStore.Application.Interfaces.Persistence;

namespace MovieStore.Application.Features.Movies.Queries.GetInactiveMovies
{
    public class GetInactiveMoviesQueryHandler : IRequestHandler<GetInactiveMoviesQuery, List<MovieDto>>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetInactiveMoviesQueryHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MovieDto>> Handle(GetInactiveMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _context.Movies
                .IgnoreQueryFilters()
                .Where(m => !m.IsActive) // kesinlikle false
                .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return movies;
        }
    }
}