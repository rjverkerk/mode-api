using mode_api.Domain;
using mode_api.Domain.DomainModel.Confederates.BattleLanguage;

namespace mode_api.data.Repositories.Confederates.BattleLanguage
{
    public class ContextDetailRepository : BaseRepository<ContextDetail>, IContextDetailRepository
    {
        public ContextDetailRepository(ApplicationContext context) : base(context) {}
    }
}
