using ConsoleApp1;
using NLog;
using RecExporter.Code.Interfaces;
using System;

namespace RecExporter.Code.Classes
{

    public class ServerLogger :Disposing, IServerLogger
    {
        private string defaultUser = "server";
        private readonly Logger log = LogManager.GetCurrentClassLogger();

        public void LogInfo(string logEvent, string message = "", string user = "")
        {
            Log(LogLevel.Info, logEvent, message, user);
        }

        public void LogWarning(string logEvent, string message = "", string user = "")
        {
            Log(LogLevel.Warn, logEvent, message, user);
        }

        public void LogError(string logEvent, string message = "", string user = "")
        {
            Log(LogLevel.Error, logEvent, message, user);
        }

        private void Log(LogLevel level, string logEvent, string message, string user = "")
        {
            LogEventInfo theEvent = new LogEventInfo(level, logEvent, message);
            theEvent.Properties["userId"] = string.IsNullOrEmpty(user) ? defaultUser : user;
            log.Log(theEvent);
        }

        public override void DisposingMethod()
        {
            base.DisposingMethod();
        }
    }

    public class cos
    {
    }
}