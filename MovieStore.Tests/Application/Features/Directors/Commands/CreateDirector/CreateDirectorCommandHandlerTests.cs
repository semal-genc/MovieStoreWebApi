using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Directors.Commands.CreateDirector;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateDirectorCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenDirectorAlreadyExists_InvalidOperationException_ShouldBeThrown()
        {
            var director = new Director
            {
                FirstName = "Christopher",
                LastName = "Nolan"
            };

            _context.Directors.Add(director);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new CreateDirectorCommand
            {
                FirstName = "Christopher",
                LastName = "Nolan"
            };

            var handler = new CreateDirectorCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Bu yönetmen zaten mevcut.");
        }

        [Fact]
        public async Task WhenValidInputsAreGiven_Director_ShouldBeCreatedAndIdReturned()
        {
            var command = new CreateDirectorCommand
            {
                FirstName = "Quentin",
                LastName = "Tarantino"
            };

            var handler = new CreateDirectorCommandHandler(_context, _mapper);

            var directorId = await handler.Handle(command, CancellationToken.None);

            var director = await _context.Directors.SingleOrDefaultAsync(x => x.Id == directorId);

            director.Should().NotBeNull();
            director.FirstName.Should().Be(command.FirstName);
            director.LastName.Should().Be(command.LastName);
        }
    }
}