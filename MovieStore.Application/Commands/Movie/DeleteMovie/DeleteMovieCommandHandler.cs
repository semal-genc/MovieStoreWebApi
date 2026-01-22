using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces;

namespace MovieStore.Application.Commands.Movie.DeleteMovie
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IMovieStoreDbContext _context;

        public DeleteMovieCommandHandler(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == request.Id && m.IsActive, cancellationToken);

            if (movie is null)
                throw new KeyNotFoundException("Silinecek film bulunamadı.");

            movie.IsActive = false;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}