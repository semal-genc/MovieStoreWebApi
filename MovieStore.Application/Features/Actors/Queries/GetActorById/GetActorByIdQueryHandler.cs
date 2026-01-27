using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Actors.Dtos;
using MovieStore.Application.Interfaces;
using MovieStore.Application.Interfaces.Persistence;

namespace MovieStore.Application.Features.Actors.Queries.GetActorById
{
    public class GetActorByIdQueryHandler : IRequestHandler<GetActorByIdQuery, ActorDetailDto>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetActorByIdQueryHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActorDetailDto> Handle(GetActorByIdQuery request, CancellationToken cancellationToken)
        {
            var actor = await _context.Actors
                .AsNoTracking()
                .Include(a => a.Movies)
                    .ThenInclude(m => m.Genre)
                .Include(a => a.Movies)
                    .ThenInclude(m => m.Director)
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (actor is null)
                throw new InvalidOperationException("Oyuncu bulunamadı.");

            return _mapper.Map<ActorDetailDto>(actor);
        }
    }
}