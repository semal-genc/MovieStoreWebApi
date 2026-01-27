using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Directors.Dtos;
using MovieStore.Application.Interfaces.Persistence;

namespace MovieStore.Application.Features.Directors.Queries.GetDirectorById
{
    public class GetDirectorByIdQueryHandler : IRequestHandler<GetDirectorByIdQuery, DirectorDetailDto>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectorByIdQueryHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DirectorDetailDto> Handle(GetDirectorByIdQuery request, CancellationToken cancellationToken)
        {
            var director = await _context.Directors
                .AsNoTracking()
                .Include(d => d.Movies)
                    .ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

            if (director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı.");

            return _mapper.Map<DirectorDetailDto>(director);
        }
    }
}