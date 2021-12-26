using mode_api_canonical.Client;
using System.Collections.Generic;

namespace mode_canonical_api.Tests.IntegrationTests.Confederates.BattleLanguageCanonical
{
    public class ModeDetailCanonicalItemComparer : IEqualityComparer<ModeDetailCanonicalItem>
    {
        public bool Equals(ModeDetailCanonicalItem x, ModeDetailCanonicalItem y) {
            if ( x.Id.Equals(y.Id) &&
                x.CreatedBy == y.CreatedBy &&
                x.CreatedDate == y.CreatedDate &&
                x.LastModifiedBy == y.LastModifiedBy &&
                x.LastModifiedDate == y.LastModifiedDate &&
                x.NameCanonical == y.NameCanonical ) {
                return true;
            }

            return false;
        }

        public int GetHashCode(ModeDetailCanonicalItem obj) {
            return obj.GetHashCode();
        }
    }
}
