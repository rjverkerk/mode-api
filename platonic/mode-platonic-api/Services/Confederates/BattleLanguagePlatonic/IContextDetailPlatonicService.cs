using mode_platonic_api.Contracts.Confederates.BattleLanguagePlatonic.ContextDetailPlatonic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mode_platonic_api.Services.Confederates.BattleLanguagePlatonic
{
    public interface IContextDetailPlatonicService
    {
        Task<ContextDetailPlatonicResponse> SearchByCriteria();

        Task<ContextDetailPlatonicItem> GetById(Guid id);

        Task Delete(IEnumerable<Guid> externalIds);

        Task<ContextDetailPlatonicItem> Update(ContextDetailPlatonicUpsert contextDetailPlatonic, Guid externalId);

        Task<ContextDetailPlatonicItem> Create(ContextDetailPlatonicUpsert contextDetailPlatonic);
    }
}
