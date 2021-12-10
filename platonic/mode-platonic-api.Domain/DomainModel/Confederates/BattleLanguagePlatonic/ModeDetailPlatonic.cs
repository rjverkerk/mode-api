using System;
using System.ComponentModel.DataAnnotations.Schema;
using mode_platonic_api.Domain.DomainModel.Common;

namespace mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguagePlatonic
{
    [Table("mode_detail_platonic")]
    public class ModeDetailPlatonic : AggregateRoot
    {
        [Column("name_platonic")]
        public string NamePlatonic { get; set; }

        public ModeDetailPlatonic() {}

        public ModeDetailPlatonic(ModeDetailPlatonicDto dto, DateTime createdDate)
        {
            ExternalId = dto.ExternalId;
            NamePlatonic = dto.NamePlatonic;
            CreatedBy = dto.ActorId;
            CreatedDate = createdDate;
        }

        public ModeDetailPlatonic Update(ModeDetailPlatonicDto dto, DateTime modifiedDate) {
            NamePlatonic = dto.NamePlatonic;
            UpdateInternal(dto.ActorId, modifiedDate);

            return this;
        }
    }
}
