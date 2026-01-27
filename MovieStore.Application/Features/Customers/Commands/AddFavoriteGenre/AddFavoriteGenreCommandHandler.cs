using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Customers.Dtos;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;

namespace MovieStore.Application.Features.Customers.Commands.AddFavoriteGenre
{
    public class AddFavoriteGenreCommandHandler : IRequestHandler<AddFavoriteGenreCommand>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public AddFavoriteGenreCommandHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddFavoriteGenreCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);

            if (customer is null)
                throw new InvalidOperationException("Alıcı bulunamadı.");

            var genre = await _context.Genres
                .FirstOrDefaultAsync(g => g.Id == request.GenreId, cancellationToken);

            if (genre is null)
                throw new InvalidOperationException("Tür bulunamadı.");

            var favoriteExists = await _context.CustomerFavoriteGenres
                .AnyAsync(f => f.CustomerId == request.CustomerId && f.GenreId == request.GenreId, cancellationToken);

            if (favoriteExists)
                throw new InvalidOperationException("Bu tür zaten favorilere eklenmiş.");

            _context.CustomerFavoriteGenres.Add(new CustomerFavoriteGenre
            {
                CustomerId = request.CustomerId,
                GenreId = request.GenreId
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}