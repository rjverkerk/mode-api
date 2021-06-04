using mode_api.Contracts.Common;

namespace mode_api.Contracts.Confederates.BattleLanguage.ContextDetail
{
    public class ContextDetailResponse
    {
        ContextDetail Context { get; set; }

        Pagination Pagination { get; set; }
    }
}