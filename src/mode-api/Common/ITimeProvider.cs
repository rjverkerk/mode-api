using System;

namespace mode_api.Common
{
    public interface ITimeProvider
    {
        DateTime UTCNow();
    }
}
