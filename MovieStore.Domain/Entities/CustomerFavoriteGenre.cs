namespace MovieStore.Domain.Entities
{
    public class CustomerFavoriteGenre
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
    }

}