using System;

namespace mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguagePlatonic
{
    public class ContextDetailPlatonicDto
    {
        public Guid ExternalId { get; set; }

        public string NameContextDetailPlatonic { get; set; }

        public int ActorId { get; set; }

        public ContextDetailPlatonicDto(Guid externalId, string nameContextDetailPlatonic, int actorId)
        {
            ExternalId = externalId;
            NameContextDetailPlatonic = nameContextDetailPlatonic;
            ActorId = actorId;
        }
    }
}
