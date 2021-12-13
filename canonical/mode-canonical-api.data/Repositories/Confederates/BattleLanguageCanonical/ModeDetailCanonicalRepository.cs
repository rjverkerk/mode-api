using mode_canonical_api.Domain;
using mode_canonical_api.Domain.DomainModel.Confederates.BattleLanguageCanonical;

namespace mode_canonical_api.data.Repositories.Confederates.BattleLanguageCanonical
{
    public class ModeDetailCanonicalRepository : BaseRepository<ModeDetailCanonical>, IModeDetailCanonicalRepository
    {
        public ModeDetailCanonicalRepository(ApplicationContext context) : base(context) {}
    }
}
