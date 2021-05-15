using KaiheilaBot.Interface;
using Serilog;
using Serilog.Core;

namespace KaiheilaBot
{
    public class LogService: ILogService
    {
        private ILogger Logger;
        public LogService()
        {
            Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File(@"Log\\Log-{Date}.txt", rollingInterval: RollingInterval.Day).CreateLogger();
        }
        public void Debug(string message)
        {
            Logger?.Debug(message);
        }

        public void Information(string message)
        {
            Logger?.Information(message);
        }

        public void Warning(string message)
        {
            Logger?.Warning(message);
        }

        public void Error(string message)
        {
            Logger?.Error(message);
        }

        public void Fatal(string message)
        {
            Logger?.Fatal(message);
        }
    }
}