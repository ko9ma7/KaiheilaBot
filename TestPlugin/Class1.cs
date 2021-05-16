using KaiheilaBot;
using KaiheilaBot.Models;
using System;
using System.Threading.Tasks;

namespace TestPlugin
{
    public class Class1 : ChannelMessageHandler
    {
        public override async Task<bool> HandleMessage(TextMessageEventArgs eventArgs)
        {
            if (eventArgs.Content.StartsWith("/"))
            {
                Console.WriteLine(eventArgs.Content);
                var result = await eventArgs.botRequest.SendGroupMessage(new ChannelMessage(eventArgs.Data, eventArgs.Data.Content));
                Console.WriteLine(result.ToString());
            }
            return true;
        }
    }
}
