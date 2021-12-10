using mode_api.Contracts.Confederates.BattleLanguage.ModeDetail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mode_api.Services.Confederates.BattleLanguage
{
    public interface IModeDetailService
    {
        Task<ModeDetailResponse> SearchByCriteria();

        Task<ModeDetailItem> GetByExternalId(Guid externalId);

        Task<bool> Delete(Guid externalId);

        Task<ModeDetailItem> Update(ModeDetailUpsert modeDetail, Guid externalId);

        Task<ModeDetailItem> Create(ModeDetailUpsert modeDetail);
    }
}
