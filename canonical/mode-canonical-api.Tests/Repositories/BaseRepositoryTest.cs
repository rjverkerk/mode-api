using Microsoft.EntityFrameworkCore;
using mode_canonical_api.Domain;

namespace mode_canonical_api.Tests.Repositories
{
    public abstract class BaseRepositoryTest
    {
        protected DbContextOptions<ApplicationContext> ContextOptions { get; }

        public BaseRepositoryTest(DbContextOptions<ApplicationContext> contextOptions) {
            ContextOptions = contextOptions;
        }
    }
}
