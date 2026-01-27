using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces.Persistence;

namespace MovieStore.Application.Features.Movies.Commands.RestoreMovie
{
    public class RestoreMovieCommandHandler : IRequestHandler<RestoreMovieCommand, int>
    {
        private readonly IMovieStoreDbContext _context;

        public RestoreMovieCommandHandler(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(RestoreMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (movie is null)
                throw new KeyNotFoundException("Böyle bir film bulunamadı.");

            if (movie.IsActive)
                throw new InvalidOperationException("Film zaten aktif durumda.");

            movie.IsActive = true;

            await _context.SaveChangesAsync(cancellationToken);

            return movie.Id;
        }
    }
}