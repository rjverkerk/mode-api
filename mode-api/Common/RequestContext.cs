﻿namespace mode_api.Common
{
    public class MockRequestContext : IRequestContext
    {
        int IRequestContext.UserId { get => 1; }
    }
}
