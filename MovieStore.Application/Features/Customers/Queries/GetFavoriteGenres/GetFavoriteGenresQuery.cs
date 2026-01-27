using MediatR;
using MovieStore.Application.Features.Customers.Dtos;

namespace MovieStore.Application.Features.Customers.Queries.GetFavoriteGenres
{
    public class GetFavoriteGenresQuery : IRequest<List<FavoriteGenreDto>>
    {
        public int CustomerId { get; set; }

        public GetFavoriteGenresQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
