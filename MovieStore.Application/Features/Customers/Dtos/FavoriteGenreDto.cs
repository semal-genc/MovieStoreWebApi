namespace MovieStore.Application.Features.Customers.Dtos
{
    public class FavoriteGenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class AddFavoriteGenreDto
    {
        public int CustomerId { get; set; }
        public int GenreId { get; set; }
    }
}
