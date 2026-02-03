using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Actors.Commands.UpdateActor;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Actors.Commands.UpdateActor
{
    public class UpdateActorCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateActorCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenActorDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var command = new UpdateActorCommand
            {
                Id = 999,
                FirstName = "Test",
                LastName = "Actor"
            };

            var handler = new UpdateActorCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Oyuncu bulunamadı.");
        }

        [Fact]
        public async Task WhenActorNameIsSame_InvalidOperationException_ShouldBeThrown()
        {
            var actor = new Actor
            {
                FirstName = "Brad",
                LastName = "Pitt"
            };

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new UpdateActorCommand
            {
                Id = actor.Id,
                FirstName = "brad",
                LastName = "pitt"
            };

            var handler = new UpdateActorCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Herhangi bir değişiklik yapılmadı.");
        }

        [Fact]
        public async Task WhenValidInputsAreGiven_Actor_ShouldBeUpdated()
        {
            var actor = new Actor
            {
                FirstName = "Tom",
                LastName = "Cruise"
            };

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new UpdateActorCommand
            {
                Id = actor.Id,
                FirstName = "Thomas",
                LastName = "Cruise"
            };

            var handler = new UpdateActorCommandHandler(_context, _mapper);

            await handler.Handle(command, CancellationToken.None);

            var updatedActor = await _context.Actors.SingleAsync(x => x.Id == actor.Id);

            updatedActor.FirstName.Should().Be(command.FirstName);
            updatedActor.LastName.Should().Be(command.LastName);
        }
    }
}