using System;
namespace mode_api.DomainModel.Common
{
    public class AggregateRoot
    {
        public int Id { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public long Version { get; set; }
    }
}
