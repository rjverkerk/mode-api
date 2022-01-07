using System;
using System.ComponentModel.DataAnnotations.Schema;
using mode_api.Domain.DomainModel.Common;

namespace mode_api.Domain.DomainModel.Confederates.BattleLanguage
{
  [Table("mode_detail")]
  public class ModeDetail : AggregateRoot
  {
    [Column("name")]
    public string Name { get; set; }

    public ModeDetail() { }

    public ModeDetail(ModeDetailDto dto, DateTime createdDate)
    {
        ExternalId = dto.ExternalId;
        Name = dto.Name;
        Order = dto.Order;
        CreatedBy = dto.ActorId;
        CreatedDate = createdDate;
     }

    public ModeDetail Update(ModeDetailDto dto, DateTime modifiedDate)
    {
        Name = dto.Name;
        Order = dto.Order;
        UpdateInternal(dto.ActorId, modifiedDate);

        return this;
    }
  }
}
