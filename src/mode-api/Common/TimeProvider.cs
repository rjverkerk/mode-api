using System;

namespace mode_api.Common
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime UTCNow() {
            return DateTime.UtcNow;
        }
    }
}
