using System;
using System.Collections.Generic;
using System.Text;

namespace KaiheilaBot.Interface
{
    /// <summary>
    /// 日志处理器
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);
        /// <summary>
        /// 输出资讯日志
        /// </summary>
        /// <param name="message"></param>
        void Information(string message);
        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="message"></param>
        void Warning(string message);
        /// <summary>
        /// 输出错误日志
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
        /// <summary>
        /// 输出严重出错日志
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string message);
    }
}
