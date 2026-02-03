using FluentValidation.TestHelper;
using MovieStore.Application.Features.Movies.Commands.UpdateMovie;
using MovieStore.Application.Features.Movies.Validators.UpdateMovie;

namespace MovieStore.Tests.Application.Features.Movies.Validators.UpdateMovie
{
    public class UpdateMovieCommandValidatorTests
    {
        private readonly UpdateMovieCommandValidator _validator;

        public UpdateMovieCommandValidatorTests()
        {
            _validator = new UpdateMovieCommandValidator();
        }

        private UpdateMovieCommand CreateValidCommand()
        {
            return new UpdateMovieCommand
            {
                Id = 1,
                Name = "Valid Movie",
                Year = 2000,
                Price = 50,
                GenreId = 1,
                DirectorId = 1
            };
        }

        [Theory]
        [InlineData(0, "Geçersiz film Id'si.")]
        [InlineData(-1, "Geçersiz film Id'si.")]
        public void WhenIdIsInvalid_ValidationError_ShouldBeReturned(int id, string expectedMessage)
        {
            var command = CreateValidCommand();
            command.Id = id;

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage(expectedMessage);
        }

        [Theory]
        [InlineData("", "Film adı boş olamaz.")]
        [InlineData("A", "Film adı en az 2 karakter olmalıdır.")]
        public void WhenNameIsInvalid_ValidationError_ShouldBeReturned(string name, string expectedMessage)
        {
            var command = CreateValidCommand();
            command.Name = name;

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorMessage(expectedMessage);
        }

        [Theory]
        [InlineData(1900, "Film yılı 1900'dan büyük olmalıdır.")]
        [InlineData(3000, "Film yılı gelecek bir yıl olamaz.")]
        public void WhenYearIsInvalid_ValidationError_ShouldBeReturned(int year, string expectedMessage)
        {
            var command = CreateValidCommand();
            command.Year = year;

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Year)
                  .WithErrorMessage(expectedMessage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void WhenPriceIsInvalid_ValidationError_ShouldBeReturned(decimal price)
        {
            var command = CreateValidCommand();
            command.Price = price;

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Price)
                  .WithErrorMessage("Film fiyatı 0'dan büyük olmalıdır.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenGenreIdIsInvalid_ValidationError_ShouldBeReturned(int genreId)
        {
            var command = CreateValidCommand();
            command.GenreId = genreId;

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.GenreId)
                  .WithErrorMessage("Tür bilgisi seçilmelidir.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenDirectorIdIsInvalid_ValidationError_ShouldBeReturned(int directorId)
        {
            var command = CreateValidCommand();
            command.DirectorId = directorId;

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.DirectorId)
                  .WithErrorMessage("Yönetmen bilgisi seçilmelidir.");
        }

        [Fact]
        public void WhenAllInputsAreValid_NoValidationError_ShouldBeReturned()
        {
            var command = CreateValidCommand();

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}