using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ActorEntity = MovieStore.Domain.Entities.Actor;
using MovieStore.Application.Interfaces;

namespace MovieStore.Application.Commands.Actor.CreateActor
{
    public class CreateActorCommandHandler : IRequestHandler<CreateActorCommand, int>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorCommandHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateActorCommand request, CancellationToken cancellationToken)
        {
            var actorExists = await _context.Actors.AnyAsync(x =>
                x.FirstName == request.FirstName &&
                x.LastName == request.LastName,
                cancellationToken);

            if (actorExists)
                throw new InvalidOperationException("Bu oyuncu zaten mevcut.");

            var actor = _mapper.Map<ActorEntity>(request);

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync(cancellationToken);

            return actor.Id;
        }
    }
}