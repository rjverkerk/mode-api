using System;

namespace mode_api.Contracts.Confederates.BattleLanguage.ModeDetail
{
    public class ModeDetailItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public int Order { get; set; }

        public long Version { get; set; }
    }
}
