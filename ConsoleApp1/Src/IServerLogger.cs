
namespace RecExporter.Code.Interfaces
{
    public interface IServerLogger
    {
        void LogInfo(string logEvent, string message = "");
        void LogWarning(string logEvent, string message = "");
        void LogError(string logEvent, string message = "");
    }
}