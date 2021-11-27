using System.Collections.Generic;

namespace mode_api.Contracts.Confederates.BattleLanguage.ModeDetail
{
    public class ModeDetailResponse
    {
        public IEnumerable<ModeDetailItem> ModeDetails { get; set; }
    }
}
