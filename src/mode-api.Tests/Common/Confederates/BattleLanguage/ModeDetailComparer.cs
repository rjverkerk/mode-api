using mode_api.Domain.DomainModel.Confederates.BattleLanguage;
using System.Collections.Generic;

namespace mode_api.Tests.Common.Confederates.BattleLanguage
{
    public class ModeDetailComparer : IEqualityComparer<ModeDetail>
    {
        public bool Equals(ModeDetail x, ModeDetail y) {
            if (x.ExternalId.Equals(y.ExternalId) &&
                x.CreatedBy == y.CreatedBy &&
                x.CreatedDate == y.CreatedDate &&
                x.LastModifiedBy == y.LastModifiedBy &&
                x.LastModifiedDate == y.LastModifiedDate &&
                x.Name == y.Name) 
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(ModeDetail obj) {
            return obj.GetHashCode();
        }
    }
}
