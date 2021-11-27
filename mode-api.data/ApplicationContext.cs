using Microsoft.EntityFrameworkCore;
using mode_api.Domain.DomainModel.Confederates.BattleLanguage;

namespace mode_api.Domain
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base() {}

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
        }
        public DbSet<ModeDetail> ModeDetails { get; set; }
    }
}
