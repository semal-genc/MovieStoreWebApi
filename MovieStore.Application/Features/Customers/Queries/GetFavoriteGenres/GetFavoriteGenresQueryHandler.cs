using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Customers.Dtos;
using MovieStore.Application.Interfaces.Persistence;

namespace MovieStore.Application.Features.Customers.Queries.GetFavoriteGenres
{
    public class GetFavoriteGenresQueryHandler : IRequestHandler<GetFavoriteGenresQuery, List<FavoriteGenreDto>>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetFavoriteGenresQueryHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FavoriteGenreDto>> Handle(GetFavoriteGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await _context.CustomerFavoriteGenres
                .Where(f => f.CustomerId == request.CustomerId)
                .Include(f => f.Genre)
                .Select(f => new FavoriteGenreDto
                {
                    Id = f.Genre.Id,
                    Name = f.Genre.Name
                })
                .ToListAsync(cancellationToken);

            return genres;
        }
    }
}