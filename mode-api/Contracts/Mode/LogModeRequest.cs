using System;

namespace mode_api.Contracts.Mode
{
    public class LogModeRequest
    {
        public string ModeId { get; set; }

        public string ContextId { get; set; }

        public int ActorId { get; set; }

        public DateTime LogDate { get; set; }
    }
}
