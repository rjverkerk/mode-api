using System;
using System.ComponentModel.DataAnnotations.Schema;
using mode_api.Domain.DomainModel.Common;

namespace mode_api.Domain.DomainModel.Confederates.BattleLanguage
{
    [Table("mode_detail")]
    public class ModeDetail : AggregateRoot
    {

        [Column("name_platonic")]
        public string Name { get; set; }

        public ModeDetail() { }

        public ModeDetail(ModeDetailDto dto)
        {
            ExternalId = dto.ExternalId;
            Name = dto.Name;
            CreatedBy = dto.ActorId;
            CreatedDate = DateTime.Now;
        }

        public ModeDetail Update(ModeDetailDto dto) {
            Name = dto.Name;
            UpdateInternal(dto);

            return this;
        }

        private void UpdateInternal(ModeDetailDto dto) {
            LastModifiedDate = DateTime.UtcNow;
            LastModifiedBy = dto.ActorId;
            base.UpdateInternal();
        }
    }
}
