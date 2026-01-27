using MediatR;

namespace MovieStore.Application.Features.Orders.Commands.BuyMovie
{
    public class BuyMovieCommand : IRequest
    {
        public int MovieId { get; set; }
        public int CustomerId { get; set; }
    }
}
