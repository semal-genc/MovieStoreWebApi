using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces;
using MovieStore.Application.Interfaces.Persistence;

namespace MovieStore.Application.Features.Actors.Commands.UpdateActor
{
    public class UpdateActorCommandHandler : IRequestHandler<UpdateActorCommand>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateActorCommandHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateActorCommand request, CancellationToken cancellationToken)
        {
            var actor = await _context.Actors
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (actor is null)
                throw new InvalidOperationException("Oyuncu bulunamadı.");

            if (actor.FirstName.Equals(request.FirstName, StringComparison.OrdinalIgnoreCase) &&
                actor.LastName.Equals(request.LastName, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Herhangi bir değişiklik yapılmadı.");
                
            _mapper.Map(request, actor);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}