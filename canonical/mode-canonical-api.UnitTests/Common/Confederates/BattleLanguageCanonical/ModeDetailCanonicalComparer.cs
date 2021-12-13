using mode_canonical_api.Domain.DomainModel.Confederates.BattleLanguageCanonical;
using System.Collections.Generic;

namespace mode_canonical_api.UnitTests.Common.Confederates.BattleLanguageCanonical
{
    class ModeDetailCanonicalComparer : IEqualityComparer<ModeDetailCanonical>
    {
        public bool Equals(ModeDetailCanonical x, ModeDetailCanonical y) {
            if (x.ExternalId.Equals(y.ExternalId) &&
                x.CreatedBy == y.CreatedBy &&
                x.CreatedDate == y.CreatedDate &&
                x.LastModifiedBy == y.LastModifiedBy &&
                x.LastModifiedDate == y.LastModifiedDate &&
                x.NameCanonical == y.NameCanonical) 
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(ModeDetailCanonical obj) {
            return obj.GetHashCode();
        }
    }
}
