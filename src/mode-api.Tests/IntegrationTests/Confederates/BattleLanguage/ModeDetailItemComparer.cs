using mode_api_canonical.Client;
using System.Collections.Generic;

namespace mode_api.Tests.IntegrationTests.Confederates.BattleLanguage
{
    public class ModeDetailItemComparer : IEqualityComparer<ModeDetailItem>
    {
        public bool Equals(ModeDetailItem x, ModeDetailItem y) {
            if ( x.Id.Equals(y.Id) &&
                x.CreatedBy == y.CreatedBy &&
                x.CreatedDate == y.CreatedDate &&
                x.LastModifiedBy == y.LastModifiedBy &&
                x.LastModifiedDate == y.LastModifiedDate &&
                x.Name == y.Name ) {
                return true;
            }

            return false;
        }

        public int GetHashCode(ModeDetailItem obj) {
            return obj.GetHashCode();
        }
    }
}
