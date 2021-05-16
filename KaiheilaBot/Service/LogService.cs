using KaiheilaBot.Interface;
using Serilog;

namespace KaiheilaBot
{
    public class LogService: ILogService
    {
        private ILogger FileLogger;
        private ILogger ConsoleLogger;
        public LogService()
        {
            FileLogger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File(@"Log\\Log.txt", rollingInterval: RollingInterval.Day, shared: true).CreateLogger();
            ConsoleLogger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();
        }
        public void Debug(string message)
        {
            FileLogger?.Debug(message);
            ConsoleLogger?.Debug(message);
        }

        public void Information(string message)
        {
            FileLogger?.Information(message);
            ConsoleLogger?.Information(message);
        }

        public void Warning(string message)
        {
            FileLogger?.Warning(message);
            ConsoleLogger?.Warning(message);
        }

        public void Error(string message)
        {
            FileLogger?.Error(message);
            ConsoleLogger?.Error(message);
        }

        public void Fatal(string message)
        {
            FileLogger?.Fatal(message);
            ConsoleLogger?.Fatal(message);
        }
    }
}