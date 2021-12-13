using System;

namespace mode_canonical_api.Common
{
    public interface ITimeProvider
    {
        DateTime UTCNow();
    }
}
