namespace MovieStore.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        public int DirectorId { get; set; }
        public Director Director { get; set; } = null!;
        
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}