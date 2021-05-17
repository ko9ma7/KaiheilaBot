using KaiheilaBot.Models;
using SimpleInjector;
using System.Threading.Tasks;

namespace KaiheilaBot.Interface
{
    /// <summary>
    /// 插件
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Handle什么功能
        /// </summary>
        public EventType HandleType { get; }
        /// <summary>
        /// Handle主要功能，如果不想后续的插件继续处理消息则return true
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public Task<bool> Handle(MessageEventArgs eventArgs);
        /// <summary>
        /// 插件启动的时候可进行准备工作
        /// </summary>
        /// <param name="container">SimpleInjector DI Container</param>
        /// <returns></returns>
        public Task PluginLoad(Container container);
        /// <summary>
        /// 插件关闭的时候可进行收拾工作
        /// </summary>
        /// <param name="container">SimpleInjector DI Container</param>
        /// <returns></returns>
        public Task PluginUnload(Container container);
    }
}
