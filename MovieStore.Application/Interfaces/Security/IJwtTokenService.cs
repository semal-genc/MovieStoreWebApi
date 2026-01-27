using MovieStore.Domain.Entities;

namespace MovieStore.Application.Interfaces.Security
{
    public interface IJwtTokenService
    {
        string CreateToken(Customer customer);
    }
}
