using FluentValidation;
using MovieStore.Application.Features.Customers.Commands.AddFavoriteGenre;

namespace MovieStore.Application.Features.Customers.Validators.AddFavoriteGenre
{
    public class AddFavoriteGenreCommandValidator : AbstractValidator<AddFavoriteGenreCommand>
    {
        public AddFavoriteGenreCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Geçerli bir CustomerId girilmelidir.");

            RuleFor(x => x.GenreId)
                .GreaterThan(0).WithMessage("Geçerli bir GenreId girilmelidir.");
        }
    }
}
