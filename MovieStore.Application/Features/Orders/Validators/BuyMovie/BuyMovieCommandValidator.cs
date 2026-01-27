using FluentValidation;
using MovieStore.Application.Features.Orders.Commands.BuyMovie;

namespace MovieStore.Application.Features.Orders.Validators.BuyMovie
{
    public class BuyMovieCommandValidator : AbstractValidator<BuyMovieCommand>
    {
        public BuyMovieCommandValidator()
        {
            RuleFor(x => x.MovieId)
                .GreaterThan(0);
        }
    }
}