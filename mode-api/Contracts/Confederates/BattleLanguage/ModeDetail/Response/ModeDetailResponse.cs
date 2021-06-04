using mode_api.Contracts.Common;

namespace mode_api.Contracts.Confederates.BattleLanguage.ModeDetail
{
    public class ModeDetailResponse
    {
        ModeDetail Mode { get; set; }

        Pagination Pagination { get; set; }
    }
}