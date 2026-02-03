using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MovieStore.Application.Features.Actors.Queries.GetActorById;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Actors.Queries.GetActorById
{
    public class GetActorByIdQueryHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetActorByIdQueryHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenActorExists_ActorDetailDto_ShouldBeReturned()
        {
            var actor = new Actor
            {
                FirstName = "Robert",
                LastName = "Downey",
                Movies = new List<Movie>
                {
                    new Movie
                    {
                        Name = "Iron Man",
                        Year = 2008,
                        Price = 50,
                        IsActive = true,
                        Genre = new Genre { Name = "Action" },
                        Director = new Director { FirstName = "Jon", LastName = "Favreau" }
                    }
                }
            };

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync(CancellationToken.None);

            var query = new GetActorByIdQuery(actor.Id);
            var handler = new GetActorByIdQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.FullName.Should().Be($"{actor.FirstName} {actor.LastName}");
            result.Movies.Should().HaveCount(1);

            var resultMovie = result.Movies.ElementAt(0);
            var actorMovie = actor.Movies.ElementAt(0);

            resultMovie.Name.Should().Be(actorMovie.Name);
            resultMovie.GenreName.Should().Be(actorMovie.Genre.Name);
            resultMovie.DirectorFullName.Should().Be($"{actorMovie.Director.FirstName} {actorMovie.Director.LastName}");
        }

        [Fact]
        public async Task WhenActorDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var query = new GetActorByIdQuery(999);
            var handler = new GetActorByIdQueryHandler(_context, _mapper);

            Func<Task> act = async () => await handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Oyuncu bulunamadı.");
        }
    }
}