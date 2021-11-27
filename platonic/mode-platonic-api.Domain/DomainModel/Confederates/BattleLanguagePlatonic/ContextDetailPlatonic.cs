using System;
using System.ComponentModel.DataAnnotations.Schema;
using mode_platonic_api.Domain.DomainModel.Common;

namespace mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguagePlatonic
{
    [Table("context_detail_platonic")]
    public class ContextDetailPlatonic : AggregateRoot
    {

        [Column("name_platonic")]
        public string NameContextDetailPlatonic { get; set; }

        public ContextDetailPlatonic() { }

        public ContextDetailPlatonic(ContextDetailPlatonicDto dto)
        {
            ExternalId = dto.ExternalId;
            NameContextDetailPlatonic = dto.NameContextDetailPlatonic;
            CreatedBy = dto.ActorId;
            CreatedDate = DateTime.Now;
        }

        public ContextDetailPlatonic Update(ContextDetailPlatonicDto dto) {
            NameContextDetailPlatonic = dto.NameContextDetailPlatonic;
            UpdateInternal(dto);

            return this;
        }

        private void UpdateInternal(ContextDetailPlatonicDto dto) {
            LastModifiedDate = DateTime.UtcNow;
            LastModifiedBy = dto.ActorId;
            base.UpdateInternal();
        }
    }
}
