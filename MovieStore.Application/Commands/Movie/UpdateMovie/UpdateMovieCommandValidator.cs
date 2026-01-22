using FluentValidation;

namespace MovieStore.Application.Commands.Movie.UpdateMovie
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(x=>x.Id)
                .GreaterThan(0).WithMessage("Geçersiz film Id'si.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Film adı boş olamaz.")
                .MinimumLength(2).WithMessage("Film adı en az 2 karakter olmalı.")
                .MaximumLength(100).WithMessage("Film adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Year)
                .GreaterThan(1900).WithMessage("'Year' değeri '1900' değerinden büyük olmalı.")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("'Year' değeri bu yıldan büyük olamaz.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("'Price' değeri '0' değerinden büyük olmalı.");
            
            RuleFor(x => x.GenreId)
                .GreaterThan(0).WithMessage("'Genre Id' değeri '0' değerinden büyük olmalı.");

            RuleFor(x => x.DirectorId)
                .GreaterThan(0).WithMessage("'Director Id' değeri '0' değerinden büyük olmalı.");
        }
    }
}