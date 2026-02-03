using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;

namespace MovieStore.Application.Features.Orders.Commands.BuyMovie
{
    public class BuyMovieCommandHandler : IRequestHandler<BuyMovieCommand>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public BuyMovieCommandHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(BuyMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.MovieId, cancellationToken);

            if (movie is null)
                throw new InvalidOperationException("Film bulunamadı.");

            var customerExists = await _context.Customers
                .AnyAsync(x => x.Id == request.CustomerId, cancellationToken);

            if (!customerExists)
                throw new InvalidOperationException("Müşteri bulunamadı.");

            var alreadyPurchased = await _context.Orders.AnyAsync(x =>
                x.MovieId == request.MovieId &&
                x.CustomerId == request.CustomerId,
                cancellationToken);

            if (alreadyPurchased)
                throw new InvalidOperationException("Bu film daha önce satın alınmış.");

            var order = _mapper.Map<Order>(request);
            order.Price = movie.Price;
            order.PurchaseDate = DateTime.UtcNow;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
