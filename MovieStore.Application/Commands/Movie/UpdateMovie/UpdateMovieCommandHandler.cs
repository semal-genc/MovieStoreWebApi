using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces;

namespace MovieStore.Application.Commands.Movie.UpdateMovie
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, int>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateMovieCommandHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == request.Id && m.IsActive, cancellationToken);

            if (movie is null)
                throw new KeyNotFoundException("Böyle bir film bulunamadı.");

            var directorExists = await _context.Directors
                .AnyAsync(d => d.Id == request.DirectorId, cancellationToken);

            if (!directorExists)
                throw new InvalidOperationException("Böyle bir yönetmen bulunamadı.");

            var genreExists = await _context.Genres
                .AnyAsync(g => g.Id == request.GenreId, cancellationToken);

            if (!genreExists)
                throw new InvalidOperationException("Böyle bir tür bulunamadı.");

            var duplicateExists = await _context.Movies
                .AnyAsync(m =>
                    m.Name.ToLower() == request.Name.ToLower() &&
                    m.Year == request.Year &&
                    m.DirectorId == request.DirectorId &&
                    m.IsActive, cancellationToken
                );

            if (duplicateExists)
                throw new InvalidOperationException("Bu film zaten mevcut.");

            _mapper.Map(request, movie);

            await _context.SaveChangesAsync(cancellationToken);

            return movie.Id;
        }
    }
}