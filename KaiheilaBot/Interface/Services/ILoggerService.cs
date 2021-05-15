using System;
using System.Collections.Generic;
using System.Text;

namespace KaiheilaBot.Interface
{
    public interface ILogService
    {
        void Debug(string message);
        void Information(string message);
        void Warning(string message);
        void Error(string message);
        void Fatal(string message);
    }
}
