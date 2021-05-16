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

        public async Task Handle(MessageEventArgs eventArgs)
        {
            var t = eventArgs as TextMessageEventArgs;
            if (t.Content.StartsWith("/"))
            {
                Console.WriteLine(t.Content);
                var result = await eventArgs.botRequest.SendGroupMessage(new ChannelMessage(eventArgs.Data, eventArgs.Data.Content));
                Console.WriteLine(result.ToString());
            }
        }
    }
}
