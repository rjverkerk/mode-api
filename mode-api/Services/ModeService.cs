using System;
using System.IO;
using mode_api.Contracts.Mode;

namespace mode_api.Services
{
    public class ModeService : IModeService
    {
        public static string LogFileName = $"log-{Guid.NewGuid()}.txt";

        public void LogMode(LogModeRequest request)
        {
            using (StreamWriter log = File.AppendText(LogFileName))
            {
                log.WriteLine($"LogMode|{request.ActorId}|{request.ContextId}|{request.LogDate.Ticks}|${request.ModeId}");
            }

            Console.WriteLine($"LogMode|{request.ActorId}|{request.ContextId}|{request.LogDate.Ticks}|${request.ModeId}");

        }
    }
}
