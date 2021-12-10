using System;

namespace mode_platonic_api.Common
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime UTCNow() {
            return DateTime.UtcNow;
        }
    }
}
