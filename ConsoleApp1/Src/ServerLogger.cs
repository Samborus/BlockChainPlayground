using ConsoleApp1;
using NLog;
using RecExporter.Code.Interfaces;
using System;

namespace RecExporter.Code.Classes
{
    public class ServerLogger : Disposing, IServerLogger
    {
        private readonly string user = "server";
        private readonly Logger log = LogManager.GetCurrentClassLogger();

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
            LogEventInfo theEvent = new LogEventInfo(level, logEvent, message);
            theEvent.Properties["userId"] = user;
            log.Log(theEvent);
        }

        public override void DisposingMethod()
        {
            base.DisposingMethod();
        }
    }
}