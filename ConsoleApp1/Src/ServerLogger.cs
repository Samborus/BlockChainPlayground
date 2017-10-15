using NLog;
using RecExporter.Code.Interfaces;

namespace RecExporter.Code.Classes
{
    public class ServerLogger : IServerLogger
    {
        private static readonly string user = "server";

        public void LogInfo(string logEvent, string message = "")
        {
            Log(LogLevel.Info, logEvent, message);
        }

        public void LogWarning(string logEvent, string message = "")
        {
            Log(LogLevel.Warn, logEvent, message);
        }

        public void LogError(string logEvent, string message = "")
        {
            Log(LogLevel.Error, logEvent, message);
        }

        private void Log(LogLevel level, string logEvent, string message)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            LogEventInfo theEvent = new LogEventInfo(level, logEvent, message);
            theEvent.Properties["userId"] = user;
            log.Log(theEvent);
        }
    }
}