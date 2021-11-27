using mode_api.Contracts.Confederates.BattleLanguage.ContextDetail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mode_api.Services.Confederates.BattleLanguage
{
    public interface IContextDetailService
    {
        Task<ContextDetailResponse> SearchByCriteria();

        Task<ContextDetailItem> GetById(Guid id);

        Task Delete(IEnumerable<Guid> externalIds);

        Task<ContextDetailItem> Update(ContextDetailUpsert contextDetail, Guid externalId);

        Task<ContextDetailItem> Create(ContextDetailUpsert contextDetail);
    }
}
