using System;

namespace mode_canonical_api.Domain.DomainModel.Confederates.BattleLanguageCanonical
{
    public class ModeDetailCanonicalDto
    {
        public Guid ExternalId { get; set; }

        public string NameCanonical { get; set; }

        public int ActorId { get; set; }

        public ModeDetailCanonicalDto(Guid externalId, string nameCanonical, int actorId)
        {
            ExternalId = externalId;
            NameCanonical = nameCanonical;
            ActorId = actorId;
        }
    }
}
