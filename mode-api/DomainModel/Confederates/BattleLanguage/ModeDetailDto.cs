namespace mode_api.DomainModel.Confederates.BattleLanguage
{
    public class ModeDetailDto
    {
        public string ExternalId { get; set; }

        public string Name { get; set; }

        public int ActorId { get; set; }

        public ModeDetailDto(string externalId, string name, int actorId)
        {
            ExternalId = externalId;
            Name = name;
            ActorId = actorId;
        }
    }
}
