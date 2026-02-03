using FluentValidation.TestHelper;
using MovieStore.Application.Features.Movies.Commands.CreateMovie;
using MovieStore.Application.Features.Movies.Validators.CreateMovie;

namespace MovieStore.Tests.Application.Features.Movies.Validators.CreateMovie
{
    public class CreateMovieCommandValidatorTests
    {
        private readonly CreateMovieCommandValidator _validator;

        public CreateMovieCommandValidatorTests()
        {
            _validator = new CreateMovieCommandValidator();
        }

        [Theory]
        [InlineData("", "Film adı boş olamaz.")]
        [InlineData("A", "Film adı en az 2 karakter olmalıdır.")]
        public void WhenNameIsInvalid_ValidationError_ShouldBeReturned(string name, string expectedMessage)
        {
            var command = new CreateMovieCommand
            {
                Name = name,
                Year = 2020,
                Price = 50,
                GenreId = 1,
                DirectorId = 1
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorMessage(expectedMessage);
        }

        [Theory]
        [InlineData(1800, "Film yılı 1900'dan büyük olmalıdır.")]
        [InlineData(3000, "Film yılı gelecek bir yıl olamaz.")]
        public void WhenYearIsInvalid_ValidationError_ShouldBeReturned(int year, string expectedMessage)
        {
            var command = new CreateMovieCommand
            {
                Name = "Test",
                Year = year,
                Price = 50,
                GenreId = 1,
                DirectorId = 1
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Year)
                  .WithErrorMessage(expectedMessage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void WhenPriceIsInvalid_ValidationError_ShouldBeReturned(decimal price)
        {
            var command = new CreateMovieCommand
            {
                Name = "Test",
                Year = 2020,
                Price = price,
                GenreId = 1,
                DirectorId = 1
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Price)
                  .WithErrorMessage("Film fiyatı 0'dan büyük olmalıdır.");
        }

        [Fact]
        public void WhenGenreIdIsZero_ValidationError_ShouldBeReturned()
        {
            var command = new CreateMovieCommand
            {
                Name = "Test",
                Year = 2020,
                Price = 50,
                GenreId = 0,
                DirectorId = 1
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.GenreId)
                  .WithErrorMessage("Tür bilgisi seçilmelidir.");
        }

        [Fact]
        public void WhenDirectorIdIsZero_ValidationError_ShouldBeReturned()
        {
            var command = new CreateMovieCommand
            {
                Name = "Test",
                Year = 2020,
                Price = 50,
                GenreId = 1,
                DirectorId = 0
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.DirectorId)
                  .WithErrorMessage("Yönetmen bilgisi seçilmelidir.");
        }

        [Fact]
        public void WhenAllInputsAreValid_NoValidationErrors_ShouldBeReturned()
        {
            var command = new CreateMovieCommand
            {
                Name = "Inception",
                Year = 2010,
                Price = 100,
                GenreId = 1,
                DirectorId = 1
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}