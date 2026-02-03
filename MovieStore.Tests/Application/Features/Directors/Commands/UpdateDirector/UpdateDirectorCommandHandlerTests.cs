using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Directors.Commands.UpdateDirector;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Directors.Commands.UpdateDirector
{
    public class UpdateDirectorCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateDirectorCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenDirectorDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var command = new UpdateDirectorCommand
            {
                Id = 999,
                FirstName = "New",
                LastName = "Name"
            };

            var handler = new UpdateDirectorCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Yönetmen bulunamadı.");
        }

        [Fact]
        public async Task WhenDirectorFirstAndLastNameAreNotChanged_InvalidOperationException_ShouldBeThrown()
        {
            var director = new Director
            {
                FirstName = "Christopher",
                LastName = "Nolan"
            };

            _context.Directors.Add(director);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new UpdateDirectorCommand
            {
                Id = director.Id,
                FirstName = "Christopher",
                LastName = "Nolan"
            };

            var handler = new UpdateDirectorCommandHandler(_context, _mapper);

            Func<Task> act = async () => 
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Herhangi bir değişiklik yapılmadı.");
        }

        [Fact]
        public async Task WhenValidInputsAreGiven_Director_ShouldBeUpdated()
        {
            var director = new Director
            {
                FirstName = "Christopher",
                LastName = "Nolan"
            };

            _context.Directors.Add(director);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new UpdateDirectorCommand
            {
                Id = director.Id,
                FirstName = "Chris",
                LastName = "Nolan"
            };

            var handler = new UpdateDirectorCommandHandler(_context, _mapper);

            await handler.Handle(command,CancellationToken.None);

            var updatedDirector = await _context.Directors
                .SingleOrDefaultAsync(x => x.Id == director.Id);

            updatedDirector.Should().NotBeNull();
            updatedDirector!.FirstName.Should().Be(command.FirstName);
            updatedDirector!.LastName.Should().Be(command.LastName);
        }
    }
}