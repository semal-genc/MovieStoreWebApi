namespace MovieStore.Application.Features.Customers.Dtos
{
    public class LoginCustomerRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}