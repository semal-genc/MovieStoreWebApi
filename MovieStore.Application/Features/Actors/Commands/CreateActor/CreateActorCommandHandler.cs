using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;

namespace MovieStore.Application.Features.Actors.Commands.CreateActor
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

            var actor = _mapper.Map<Actor>(request);

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync(cancellationToken);

            return actor.Id;
        }
    }
}