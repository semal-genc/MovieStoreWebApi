using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using MovieStore.Application.Features.Actors.Commands.DeleteActor;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Actors.Commands.DeleteActor
{
    public class DeleteActorCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;

        public DeleteActorCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task WhenActorExists_ShouldRemoveActor()
        {
            var actor = new Actor
            {
                FirstName = "Test",
                LastName = "Actor"
            };

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new DeleteActorCommand(actor.Id);
            var handler = new DeleteActorCommandHandler(_context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().Be(Unit.Value);

            var deletedActor = await _context.Actors.FindAsync(actor.Id);
            deletedActor.Should().BeNull();
        }

        [Fact]
        public async Task WhenActorDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var command = new DeleteActorCommand(999);
            var handler = new DeleteActorCommandHandler(_context);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Oyuncu bulunamadı.");
        }
    }
}