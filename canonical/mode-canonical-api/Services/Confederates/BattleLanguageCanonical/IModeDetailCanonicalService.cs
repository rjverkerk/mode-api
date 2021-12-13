using mode_canonical_api.Contracts.Confederates.BattleLanguageCanonical.ModeDetailCanonical;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mode_canonical_api.Services.Confederates.BattleLanguageCanonical
{
    public interface IModeDetailCanonicalService
    {
        Task<ModeDetailCanonicalResponse> SearchByCriteria();

        Task<ModeDetailCanonicalItem> GetByExternalId(Guid externalId);

        Task<bool> Delete(Guid externalId);

        Task<ModeDetailCanonicalItem> Update(ModeDetailCanonicalUpsert modeDetailCanonical, Guid externalId);

        Task<ModeDetailCanonicalItem> Create(ModeDetailCanonicalUpsert modeDetailCanonical);
    }
}
