using KaiheilaBot;
using KaiheilaBot.Interface;
using KaiheilaBot.Models;
using System;
using System.Threading.Tasks;

namespace TestPlugin
{
    public class Class1 : IPlugin
    {
        public EventType HandleType => EventType.ChannelTextMessage;

        public Task PluginLoad(IServiceProvider container)
        {
            return null;
        }

        public Task PluginUnload(IServiceProvider container)
        {
            return null;
        }

        public async Task<bool> Handle(MessageEventArgs eventArgs)
        {
            var t = (TextMessageEventArgs)eventArgs;
            if (t.Content.StartsWith("/"))
            {
                Console.WriteLine(t.Content);
                var result = await eventArgs.botRequest.SendGroupMessage(new ChannelMessage(eventArgs.Data, eventArgs.Data.Content));
                Console.WriteLine(result.ToString());
            }
            return true;
        }
    }
}
