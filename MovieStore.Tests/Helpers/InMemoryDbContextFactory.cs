
using Microsoft.EntityFrameworkCore;
using MovieStore.Domain.Entities;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Tests.Helpers
{
    public static class InMemoryDbContextFactory
    {
        public static MovieStoreDbContext Create()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieStoreDbContext(options);

            context.Customers.Add(new Customer
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Email = "test@test.com",
                PasswordHash = "HASH"
            });

            context.Genres.Add(new Genre { Id = 1, Name = "Action" });
            context.SaveChanges();

            return context;
        }
    }
}