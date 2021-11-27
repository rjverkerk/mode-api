using System.Collections.Generic;

namespace mode_platonic_api.Contracts.Confederates.BattleLanguage.ContextDetail
{
    public class ContextDetailResponse
    {
        public IEnumerable<ContextDetailItem> ContextDetails { get; set; }
    }
}
