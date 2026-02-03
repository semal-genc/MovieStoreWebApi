using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MovieStore.Application.Features.Customers.Queries.GetFavoriteGenres;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Customers.Queries.GetFavoriteGenres
{
    public class GetFavoriteGenresQueryHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetFavoriteGenresQueryHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenCustomerHasFavoriteGenres_FavoriteGenreDtoList_ShouldBeReturned()
        {
            var customer = new Customer
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@test.com",
                PasswordHash = "hash"
            };

            var genre = new Genre
            {
                Name = "Action"
            };

            _context.Customers.Add(customer);
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync(CancellationToken.None);

            _context.CustomerFavoriteGenres.Add(new CustomerFavoriteGenre
            {
                CustomerId = customer.Id,
                GenreId = genre.Id
            });

            await _context.SaveChangesAsync(CancellationToken.None);

            var query = new GetFavoriteGenresQuery(customer.Id);
            var handler = new GetFavoriteGenresQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().HaveCount(1);

            result[0].Id.Should().Be(genre.Id);
            result[0].Name.Should().Be(genre.Name);
        }

        [Fact]
        public async Task WhenCustomerHasNoFavoriteGenres_EmptyList_ShouldBeReturned()
        {
            var customer = new Customer
            {
                FirstName = "Empty",
                LastName = "User",
                Email = "empty@test.com",
                PasswordHash = "hash"
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(CancellationToken.None);

            var query = new GetFavoriteGenresQuery(customer.Id);
            var handler = new GetFavoriteGenresQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}