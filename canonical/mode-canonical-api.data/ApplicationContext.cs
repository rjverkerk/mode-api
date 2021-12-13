using Microsoft.EntityFrameworkCore;
using mode_canonical_api.Domain.DomainModel.Confederates.BattleLanguageCanonical;

namespace mode_canonical_api.Domain
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base() {}

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
        }
        public DbSet<ModeDetailCanonical> ModeDetailCanonicals { get; set; }
    }
}
