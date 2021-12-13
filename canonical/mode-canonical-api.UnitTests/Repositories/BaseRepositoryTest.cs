using Microsoft.EntityFrameworkCore;
using mode_canonical_api.Domain;

namespace mode_canonical_api.UnitTests.Repositories
{
    public abstract class BaseRepositoryTest
    {
        protected DbContextOptions<ApplicationContext> ContextOptions { get; }

        public BaseRepositoryTest() {
            ContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .Options;
        }
    }
}
