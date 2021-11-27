using mode_platonic_api.Contracts.Confederates.BattleLanguagePlatonic.ModeDetailPlatonic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mode_platonic_api.Services.Confederates.BattleLanguagePlatonic
{
    public interface IModeDetailPlatonicService
    {
        Task<ModeDetailPlatonicResponse> SearchByCriteria();

        Task<ModeDetailPlatonicItem> GetById(Guid id);

        Task Delete(IEnumerable<Guid> externalIds);

        Task<ModeDetailPlatonicItem> Update(ModeDetailPlatonicUpsert modeDetailPlatonic, Guid externalId);

        Task<ModeDetailPlatonicItem> Create(ModeDetailPlatonicUpsert modeDetailPlatonic);
    }
}
