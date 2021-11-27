using System;
using mode_api.DomainModel.Common;

namespace mode_api.DomainModel.Confederates.BattleLanguage
{
    public class ModeDetail : AggregateRoot
    {
        public string ExternalId { get; set; }

        public string Name { get; set; }

        public ModeDetail(ModeDetailDto dto)
        {
            ExternalId = dto.ExternalId;
            Name = dto.Name;
            CreatedBy = dto.ActorId;
            CreatedDate = DateTime.Now;
        }
    }
}
