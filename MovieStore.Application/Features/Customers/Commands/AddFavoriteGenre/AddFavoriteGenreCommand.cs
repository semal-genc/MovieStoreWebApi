using MediatR;

namespace MovieStore.Application.Features.Customers.Commands.AddFavoriteGenre
{
    public class AddFavoriteGenreCommand : IRequest
    {
        public int CustomerId { get; set; }
        public int GenreId { get; set; }

        public AddFavoriteGenreCommand(int customerId, int genreId)
        {
            CustomerId = customerId;
            GenreId = genreId;
        }
    }
}
