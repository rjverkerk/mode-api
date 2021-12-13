using System;
using System.ComponentModel.DataAnnotations.Schema;
using mode_canonical_api.Domain.DomainModel.Common;

namespace mode_canonical_api.Domain.DomainModel.Confederates.BattleLanguageCanonical
{
  [Table("mode_detail_canonical")]
  public class ModeDetailCanonical : AggregateRoot
  {
    [Column("name_canonical")]
    public string NameCanonical { get; set; }

    public ModeDetailCanonical() { }

    public ModeDetailCanonical(ModeDetailCanonicalDto dto, DateTime createdDate)
    {
      ExternalId = dto.ExternalId;
      NameCanonical = dto.NameCanonical;
      CreatedBy = dto.ActorId;
      CreatedDate = createdDate;
    }

    public ModeDetailCanonical Update(ModeDetailCanonicalDto dto, DateTime modifiedDate)
    {
      NameCanonical = dto.NameCanonical;
      UpdateInternal(dto.ActorId, modifiedDate);

      return this;
    }
  }
}
