using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.DTOs;
using MovieStore.Application.Interfaces;

namespace MovieStore.Application.Queries.Movie.GetMovieById
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieDto>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMovieByIdQueryHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MovieDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (movie == null)
                throw new KeyNotFoundException("Böyle bir film bulunamadı.");

            return _mapper.Map<MovieDto>(movie);
        }
    }
}