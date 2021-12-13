using System;

namespace mode_canonical_api.Contracts.Confederates.BattleLanguageCanonical.ModeDetailCanonical
{
    public class ModeDetailCanonicalItem
    {
        public Guid Id { get; set; }

        public string NameCanonical { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public long Version { get; set; }
    }
}
