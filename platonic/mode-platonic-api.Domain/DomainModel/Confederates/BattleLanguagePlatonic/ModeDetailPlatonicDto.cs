using System;

namespace mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguagePlatonic
{
    public class ModeDetailPlatonicDto
    {
        public Guid ExternalId { get; set; }

        public string NamePlatonic { get; set; }

        public int ActorId { get; set; }

        public ModeDetailPlatonicDto(Guid externalId, string namePlatonic, int actorId)
        {
            ExternalId = externalId;
            NamePlatonic = namePlatonic;
            ActorId = actorId;
        }
    }
}