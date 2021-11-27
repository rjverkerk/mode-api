using System;

namespace mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguage
{
    public class ContextDetailDto
    {
        public Guid ExternalId { get; set; }

        public string Name { get; set; }

        public int ActorId { get; set; }

        public ContextDetailDto(Guid externalId, string name, int actorId)
        {
            ExternalId = externalId;
            Name = name;
            ActorId = actorId;
        }
    }
}
