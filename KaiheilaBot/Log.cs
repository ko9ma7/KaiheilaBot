using Serilog.Core;

namespace KaiheilaBot
{
    internal static class Log
    {
        internal static Logger Logger;

        internal static void Debug(string message)
        {
            Logger?.Debug(message);
        }
        
        internal static void Information(string message)
        {
            Logger?.Information(message);
        }
        
        internal static void Warning(string message)
        {
            Logger?.Warning(message);
        }
        
        internal static void Error(string message)
        {
            Logger?.Error(message);
        }
        
        internal static void Fatal(string message)
        {
            Logger?.Fatal(message);
        }
    }
}