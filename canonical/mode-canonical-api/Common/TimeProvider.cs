using System;

namespace mode_canonical_api.Common
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime UTCNow() {
            return DateTime.UtcNow;
        }
    }
}
