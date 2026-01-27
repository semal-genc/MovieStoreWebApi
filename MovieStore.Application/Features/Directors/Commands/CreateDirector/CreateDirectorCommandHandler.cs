using AutoMapper;
using MediatR;
using MovieStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces.Persistence;

namespace MovieStore.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, int>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateDirectorCommandHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            var diectorExists = await _context.Directors.AnyAsync(x =>
                x.FirstName == request.FirstName &&
                x.LastName == request.LastName, cancellationToken);

            if (diectorExists)
                throw new InvalidOperationException("Bu yönetmen zaten mevcut.");

            var director = _mapper.Map<Director>(request);

            _context.Directors.Add(director);
            await _context.SaveChangesAsync(cancellationToken);

            return director.Id;
        }
    }
}