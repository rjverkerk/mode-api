using System;

namespace mode_platonic_api.Common
{
    public interface ITimeProvider
    {
        DateTime UTCNow();
    }
}
