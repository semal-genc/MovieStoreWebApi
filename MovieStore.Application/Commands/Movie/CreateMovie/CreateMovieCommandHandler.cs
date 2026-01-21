
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces;

namespace MovieStore.Application.Commands.Movie.CreateMovie
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieCommandHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movieExists = await _context.Movies.AnyAsync(x =>
                x.Name.ToLower() == request.Name.ToLower() &&
                x.Year == request.Year &&
                x.DirectorId == request.DirectorId &&
                x.IsActive,
                cancellationToken);

            if (movieExists)
                throw new InvalidOperationException("Bu film zaten mevcut.");

            var directorExists = await _context.Directors
                .AnyAsync(x => x.Id == request.DirectorId, cancellationToken);

            if (!directorExists)
                throw new InvalidOperationException("Böyle bir yönetmen bulunamadı.");

            var genreExists = await _context.Genres
                .AnyAsync(x => x.Id == request.GenreId, cancellationToken);

            if (!genreExists)
                throw new InvalidOperationException("Böyle bir tür bulunamadı.");

            var movie = _mapper.Map<Domain.Entities.Movie>(request);

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(cancellationToken);

            return movie.Id;
        }
    }
}