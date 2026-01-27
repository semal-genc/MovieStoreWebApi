using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces.Persistence;

namespace MovieStore.Application.Features.Actors.Commands.DeleteActor
{
    public class DeleteActorCommandHandler : IRequestHandler<DeleteActorCommand>
    {
        private readonly IMovieStoreDbContext _context;

        public DeleteActorCommandHandler(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
        {
            var actor = await _context.Actors
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (actor is null)
                throw new InvalidOperationException("Oyuncu bulunamadı.");

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}