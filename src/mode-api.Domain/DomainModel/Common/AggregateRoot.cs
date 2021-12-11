using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace mode_api.Domain.DomainModel.Common
{
    public abstract class AggregateRoot
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("external_id")]
        public Guid ExternalId { get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("modified_by")]
        public int? LastModifiedBy { get; set; }

        [Column("modified_date")]
        public DateTime? LastModifiedDate { get; set; }

        [Column("version")]
        public long Version { get; set; } = 1;

        public AggregateRoot() {}

        protected void UpdateInternal(int actorId, DateTime modifiedDate) {
            Version = Version + 1;
            LastModifiedBy = actorId;
            LastModifiedDate = modifiedDate;
        }

    }
}
