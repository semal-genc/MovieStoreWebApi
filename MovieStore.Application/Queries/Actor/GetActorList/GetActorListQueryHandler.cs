using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.DTOs;
using MovieStore.Application.Interfaces;

namespace MovieStore.Application.Queries.Actor.GetActorList
{
    public class GetActorListQueryHandler : IRequestHandler<GetActorListQuery, List<ActorDto>>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetActorListQueryHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ActorDto>> Handle(GetActorListQuery request, CancellationToken cancellationToken)
        {
            var actor = await _context.Actors
                .AsNoTracking()
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<ActorDto>>(actor);
        }
    }
}