using System;

namespace mode_platonic_api.Contracts.Confederates.BattleLanguagePlatonic.ContextDetailPlatonic
{
    public class ContextDetailPlatonicItem
    {
        public Guid Id { get; set; }

        public string NameContextDetailPlatonic { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public long Version { get; set; }
    }
}
