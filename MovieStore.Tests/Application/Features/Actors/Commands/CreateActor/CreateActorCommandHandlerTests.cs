using AutoMapper;
using FluentAssertions;
using MovieStore.Application.Features.Actors.Commands.CreateActor;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Actors.Commands.CreateActor
{
    public class CreateActorCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenActorDoesNotExist_ShouldAddActorAndReturnId()
        {
            var command = new CreateActorCommand
            {
                FirstName = "Test",
                LastName = "Actor"
            };

            var handler = new CreateActorCommandHandler(_context, _mapper);

            var actorId = await handler.Handle(command, CancellationToken.None);

            actorId.Should().BeGreaterThan(0);

            var actorInDb = await _context.Actors.FindAsync(actorId);
            actorInDb.Should().NotBeNull();
            actorInDb.FirstName.Should().Be(command.FirstName);
            actorInDb.LastName.Should().Be(command.LastName);
        }

        [Fact]
        public async Task WhenActorAlreadyExists_InvalidOperationException_ShouldBeThrown()
        {
            _context.Actors.Add(new Actor
            {
                FirstName = "Existing",
                LastName = "Actor"
            });
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new CreateActorCommand
            {
                FirstName = "Existing",
                LastName = "Actor"
            };

            var handler = new CreateActorCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Bu oyuncu zaten mevcut.");
        }
    }
}