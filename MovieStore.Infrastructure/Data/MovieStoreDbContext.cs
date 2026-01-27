using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces;
using MovieStore.Domain.Entities;

namespace MovieStore.Infrastructure.Data
{
    public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options) { }

        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Actor> Actors => Set<Actor>();
        public DbSet<Director> Directors => Set<Director>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Genre> Genres => Set<Genre>();

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasQueryFilter(m => m.IsActive);

            modelBuilder.Entity<Movie>().HasMany(m => m.Actors).WithMany(a => a.Movies).UsingEntity(j => j.ToTable("ActorMovie"));
            modelBuilder.Entity<Customer>().HasMany(m => m.FavoriteGenres).WithMany(g => g.Customers);

            modelBuilder.Entity<Director>()
                .HasMany(d => d.Movies)
                .WithOne(m => m.Director)
                .HasForeignKey("DirectorId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Movies)
                .WithOne(m => m.Genre)
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Movie>().Property(m => m.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(m => m.Price).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Movie)
                .WithMany(m => m.Orders)
                .HasForeignKey(o => o.MovieId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Movie>()
                .HasIndex(x => new { x.Name, x.Year, x.DirectorId })
                .IsUnique();

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Drama" },
                new Genre { Id = 4, Name = "Horror" },
                new Genre { Id = 5, Name = "Sci-Fi" }
            );

            modelBuilder.Entity<Director>().HasData(
                new Director { Id = 1, FirstName = "Christopher ", LastName = "Nolan" },
                new Director { Id = 2, FirstName = "Quentin", LastName = "Tarantino" }
            );

            modelBuilder.Entity<Actor>().HasData(
                new Actor { Id = 1, FirstName = "Leonardo", LastName = "DiCaprio" },
                new Actor { Id = 2, FirstName = "Brad", LastName = "Pitt" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FirstName = "Semal",
                    LastName = "Genç",
                    Email = "semal@test.com",
                    PasswordHash = "TEMP_HASH"
                }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Name = "Interstellar",
                    Year = 2014,
                    Price = 120,
                    IsActive = true,
                    GenreId = 5,
                    DirectorId = 1
                }
            );

            modelBuilder.Entity("ActorMovie").HasData(
                new { ActorsId = 1, MoviesId = 1 },
                new { ActorsId = 2, MoviesId = 1 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    MovieId = 1,
                    CustomerId = 1,
                    Price = 120
                }
            );
        }
    }
}