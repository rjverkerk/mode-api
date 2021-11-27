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

        public ModeDetailPlatonic() { }

        public ModeDetailPlatonic(ModeDetailPlatonicDto dto)
        {
            ExternalId = dto.ExternalId;
            NamePlatonic = dto.NamePlatonic;
            CreatedBy = dto.ActorId;
            CreatedDate = DateTime.Now;
        }

        public ModeDetailPlatonic Update(ModeDetailPlatonicDto dto) {
            NamePlatonic = dto.NamePlatonic;
            UpdateInternal(dto);

            return this;
        }

        private void UpdateInternal(ModeDetailPlatonicDto dto) {
            LastModifiedDate = DateTime.UtcNow;
            LastModifiedBy = dto.ActorId;
            base.UpdateInternal();
        }
    }
}
