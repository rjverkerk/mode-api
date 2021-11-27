using System;
using System.ComponentModel.DataAnnotations.Schema;
using mode_api.Domain.DomainModel.Common;

namespace mode_api.Domain.DomainModel.Confederates.BattleLanguage
{
    [Table("context_detail")]
    public class ContextDetail : AggregateRoot
    {

        [Column("name_platonic")]
        public string Name { get; set; }

        public ContextDetail() { }

        public ContextDetail(ContextDetailDto dto)
        {
            ExternalId = dto.ExternalId;
            Name = dto.Name;
            CreatedBy = dto.ActorId;
            CreatedDate = DateTime.Now;
        }

        public ContextDetail Update(ContextDetailDto dto) {
            Name = dto.Name;
            UpdateInternal(dto);

            return this;
        }

        private void UpdateInternal(ContextDetailDto dto) {
            LastModifiedDate = DateTime.UtcNow;
            LastModifiedBy = dto.ActorId;
            base.UpdateInternal();
        }
    }
}
