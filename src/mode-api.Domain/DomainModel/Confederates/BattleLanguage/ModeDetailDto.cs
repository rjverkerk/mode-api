using System;

namespace mode_api.Domain.DomainModel.Confederates.BattleLanguage
{
    public class ModeDetailDto
    {
        public Guid ExternalId { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public int ActorId { get; set; }

        public ModeDetailDto(Guid externalId, string name, int order, int actorId)
        {
            ExternalId = externalId;
            Name = name;
            Order = order;
            ActorId = actorId;
        }
    }
}
