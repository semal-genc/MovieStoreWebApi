namespace MovieStore.Application.Features.Movies.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string GenreName { get; set; } = null!;
        public string DirectorFullName { get; set; } = null!;
    }
}