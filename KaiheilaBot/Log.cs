using Serilog.Core;

namespace KaiheilaBot
{
    public static class Log
    {
        public static Logger Logger;

        public static void Debug(string message)
        {
            Logger?.Debug(message);
        }
        
        public static void Information(string message)
        {
            Logger?.Information(message);
        }
        
        public static void Warning(string message)
        {
            Logger?.Warning(message);
        }
        
        public static void Error(string message)
        {
            Logger?.Error(message);
        }
        
        public static void Fatal(string message)
        {
            Logger?.Fatal(message);
        }
    }
}