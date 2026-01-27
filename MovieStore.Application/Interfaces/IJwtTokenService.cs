using MovieStore.Domain.Entities;

namespace MovieStore.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string CreateToken(Customer customer);
    }
}
