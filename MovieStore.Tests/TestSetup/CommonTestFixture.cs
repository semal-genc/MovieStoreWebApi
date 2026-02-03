using AutoMapper;
using MovieStore.Application.Common.Mappings;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Tests.Helpers;

namespace MovieStore.Tests.TestSetup
{
    public class CommonTestFixture : IDisposable
    {
        public IMovieStoreDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public CommonTestFixture()
        {
            var dbContext = InMemoryDbContextFactory.Create();
            Context = dbContext;

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ActorProfile>();
                cfg.AddProfile<CustomerProfile>();
                cfg.AddProfile<DirectorProfile>();
                cfg.AddProfile<MovieProfile>();
                cfg.AddProfile<OrderProfile>();
            });

            configuration.AssertConfigurationIsValid();
            Mapper = configuration.CreateMapper();
        }

        public void Dispose()
        {
            (Context as IDisposable)?.Dispose();
        }
    }
}