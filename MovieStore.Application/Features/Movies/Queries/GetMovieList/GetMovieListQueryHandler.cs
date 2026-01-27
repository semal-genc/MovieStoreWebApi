using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Movies.Dtos;
using MovieStore.Application.Interfaces.Persistence;

namespace MovieStore.Application.Features.Movies.Queries.GetMovieList
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