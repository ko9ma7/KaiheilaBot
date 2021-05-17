using KaiheilaBot.Interface;
using KaiheilaBot.Models;
using SimpleInjector;
using System.Threading.Tasks;

namespace KaiheilaBot
{
    public abstract class ChannelMessageHandler : IPlugin
    {
        public EventType HandleType => EventType.ChannelTextMessage;

        public Task<bool> Handle(MessageEventArgs eventArgs)
        {
            return HandleMessage(eventArgs as TextMessageEventArgs);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public virtual async Task PluginLoad(Container container)
        {

        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public virtual async Task PluginUnload(Container container)
        {

        }
        /// <summary>
        /// Handle主要功能，如果不想后续的插件继续处理消息则return true
        /// </summary>
        /// <param name="messageEventArgs"></param>
        /// <returns></returns>
        public abstract Task<bool> HandleMessage(TextMessageEventArgs messageEventArgs);
    }
}
