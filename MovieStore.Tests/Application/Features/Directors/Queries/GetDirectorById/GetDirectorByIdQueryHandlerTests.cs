using AutoMapper;
using FluentAssertions;
using MovieStore.Application.Features.Directors.Queries.GetDirectorById;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Directors.Queries.GetDirectorById
{
    public class GetDirectorByIdQueryHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectorByIdQueryHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenDirectorDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var query = new GetDirectorByIdQuery(999);

            var handler = new GetDirectorByIdQueryHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(query, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Yönetmen bulunamadı.");
        }

        [Fact]
        public async Task WhenDirectorExists_DirectorDetailDto_ShouldBeReturned()
        {
            var genre = new Genre
            {
                Name = "Action"
            };

            var director = new Director
            {
                FirstName = "Guy",
                LastName = "Ritchie",
                Movies = new List<Movie>
                {
                    new Movie
                    {
                        Name = "Snatch",
                        Year = 2000,
                        Price = 50,
                        Genre = genre
                    }
                }
            };

            _context.Directors.Add(director);
            await _context.SaveChangesAsync(CancellationToken.None);

            var query = new GetDirectorByIdQuery(director.Id);

            var handler=new GetDirectorByIdQueryHandler(_context,_mapper);

            var result=await handler.Handle(query,CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(director.Id);
            result.FullName.Should().Be($"{director.FirstName} {director.LastName}");

            result.Movies.Should().HaveCount(1);
            result.Movies.First().Name.Should().Be(director.Movies.First().Name);
            result.Movies.First().GenreName.Should().Be(director.Movies.First().Genre.Name);
        }
    }
}