using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces;

namespace MovieStore.Application.Commands.Director.DeleteDirector
{
    public class DeleteDirectorCommandHandler : IRequestHandler<DeleteDirectorCommand>
    {
        private readonly IMovieStoreDbContext _context;

        public DeleteDirectorCommandHandler(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            var director = await _context.Directors
                .Include(d => d.Movies)
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

            if (director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı.");

            if (director.Movies.Any())
                throw new InvalidOperationException(
                        "Bu yönetmenin filmleri bulunmaktadır. Silme işlemi yapılamaz."
                    );

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}