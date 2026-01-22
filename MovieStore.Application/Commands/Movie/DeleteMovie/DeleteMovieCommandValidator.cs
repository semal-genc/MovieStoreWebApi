using FluentValidation;

namespace MovieStore.Application.Commands.Movie.DeleteMovie
{
    public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Geçerli bir film Id'si girilmelidir.");
        }
    }
}