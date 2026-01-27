namespace MovieStore.Application.DTOs
{
    public class LoginCustomerRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}