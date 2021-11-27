using System;

namespace mode_api.Domain.DomainModel.Confederates.BattleLanguage
{
    public class ContextDetailDto
    {
        public Guid ExternalId { get; set; }

        public string Name { get; set; }

        public int ActorId { get; set; }

        public ContextDetailDto(Guid externalId, string nameContextDetail, int actorId)
        {
            ExternalId = externalId;
            Name = nameContextDetail;
            ActorId = actorId;
        }
    }
}
