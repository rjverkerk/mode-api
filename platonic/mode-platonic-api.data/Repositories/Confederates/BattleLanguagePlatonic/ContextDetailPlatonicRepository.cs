using mode_platonic_api.Domain;
using mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguagePlatonic;

namespace mode_platonic_api.data.Repositories.Confederates.BattleLanguagePlatonic
{
    public class ContextDetailPlatonicRepository : BaseRepository<ContextDetailPlatonic>, IContextDetailPlatonicRepository
    {
        public ContextDetailPlatonicRepository(ApplicationContext context) : base(context) {}
    }
}
