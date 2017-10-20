
namespace RecExporter.Code.Interfaces
{
    public interface IServerLogger
    {
        void LogInfo(string logEvent, string message = "", string user = "");
        void LogWarning(string logEvent, string message = "", string user = "");
        void LogError(string logEvent, string message = "", string user = "");
    }
}