using FluentValidation;
using MovieStore.Application.Features.Movies.Commands.UpdateMovie;

namespace MovieStore.Application.Features.Movies.Validators.UpdateMovie
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(x=>x.Id)
                .GreaterThan(0).WithMessage("Geçersiz film Id'si.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Film adı boş olamaz.")
                .MinimumLength(2).WithMessage("Film adı en az 2 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Film adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Year)
                .GreaterThan(1900).WithMessage("Film yılı 1900'dan büyük olmalıdır.")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Film yılı gelecek bir yıl olamaz.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Film fiyatı 0'dan büyük olmalıdır.");
            
            RuleFor(x => x.GenreId)
                .GreaterThan(0).WithMessage("Tür bilgisi seçilmelidir.");

            RuleFor(x => x.DirectorId)
                .GreaterThan(0).WithMessage("Yönetmen bilgisi seçilmelidir.");
        }
    }
}