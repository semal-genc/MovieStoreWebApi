using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Directors.Dtos;
using MovieStore.Application.Interfaces.Persistence;

namespace MovieStore.Application.Features.Directors.Queries.GetDirectorList
{
    public class GetDirectorListQueryHandler : IRequestHandler<GetDirectorListQuery, List<DirectorDto>>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectorListQueryHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DirectorDto>> Handle(GetDirectorListQuery request, CancellationToken cancellationToken)
        {
            var directors = await _context.Directors
                .AsNoTracking()
                .OrderBy(d => d.FirstName)
                .ThenBy(d => d.LastName)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<DirectorDto>>(directors);
        }
    }
}