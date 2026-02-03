using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces.Persistence;

namespace MovieStore.Application.Features.Directors.Commands.UpdateDirector
{
    public class UpdateDirectorCommandHandler : IRequestHandler<UpdateDirectorCommand>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateDirectorCommandHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
        {
            var director = await _context.Directors
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

            if (director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı.");

            if (director.FirstName.Equals(request.FirstName,StringComparison.OrdinalIgnoreCase) 
                && director.LastName.Equals(request.LastName, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Herhangi bir değişiklik yapılmadı.");

            _mapper.Map(request, director);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}