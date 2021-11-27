using mode_api.Domain;
using mode_api.Domain.DomainModel.Confederates.BattleLanguage;

namespace mode_api.data.Repositories.Confederates.BattleLanguage
{
    public class ModeDetailRepository : BaseRepository<ModeDetail>, IModeDetailRepository
    {
        public ModeDetailRepository(ApplicationContext context) : base(context) {}
    }
}
