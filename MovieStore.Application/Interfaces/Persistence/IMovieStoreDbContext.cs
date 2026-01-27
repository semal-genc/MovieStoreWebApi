using Microsoft.EntityFrameworkCore;
using MovieStore.Domain.Entities;

namespace MovieStore.Application.Interfaces.Persistence
{
    public interface IMovieStoreDbContext
    {
        DbSet<Movie> Movies { get; }
        DbSet<Actor> Actors { get; }
        DbSet<Director> Directors { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Order> Orders { get; }
        DbSet<Genre> Genres { get; }
        DbSet<CustomerFavoriteGenre> CustomerFavoriteGenres { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}