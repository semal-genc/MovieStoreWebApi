namespace MovieStore.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<CustomerFavoriteGenre> FavoriteGenres { get; set; } = new List<CustomerFavoriteGenre>();
    }
}