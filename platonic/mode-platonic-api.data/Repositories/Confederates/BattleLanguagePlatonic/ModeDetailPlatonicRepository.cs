using mode_platonic_api.Domain;
using mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguagePlatonic;

namespace mode_platonic_api.data.Repositories.Confederates.BattleLanguagePlatonic
{
    public class ModeDetailPlatonicRepository : BaseRepository<ModeDetailPlatonic>, IModeDetailPlatonicRepository
    {
        public ModeDetailPlatonicRepository(ApplicationContext context) : base(context) {}
    }
}
