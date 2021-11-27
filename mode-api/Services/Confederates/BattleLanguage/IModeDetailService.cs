using mode_api.Contracts.Confederates.BattleLanguage.ModeDetail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mode_api.Services.Confederates.BattleLanguage
{
    public interface IModeDetailService
    {
        Task<ModeDetailResponse> SearchByCriteria();

        Task<ModeDetailItem> GetById(Guid id);

        Task Delete(IEnumerable<Guid> externalIds);

        Task<ModeDetailItem> Update(ModeDetailUpsert modeDetail, Guid externalId);

        Task<ModeDetailItem> Create(ModeDetailUpsert modeDetail);
    }
}
