using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace KaiheilaBot.Interface
{
    public interface IConsole
    {
        public Task<JObject> SendGroupMessage(ChannelMessage message);
        public Task<JObject> CreateChannel(NewChannelMessage message);
        public Task<JObject> RemoveChannel(RemoveChannelMessage message);
        public Task<JObject> GetChannels();
        public Task<JObject> RemoveMessage(RemoveMessageMessage message);
        public Task<JObject> ChangeNick(ChangeNickMessage message);
    }
}
