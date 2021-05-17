using KaiheilaBot.Interface;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace KaiheilaBot.Service
{
    public class ConsoleService : IConsole
    {
        private IBotRequest request;
        public ConsoleService(IBotRequest request)
        {
            this.request = request;
        }

        public Task<HttpResponseMessage> ChangeNick(ChangeNickMessage message)
        {
            request.SetMethod(RestSharp.Method.POST);
            request.SetUrl("guild/nickname");
            request.AddJToken(message);
            return GetResult<HttpResponseMessage>();
        }

        public Task<HttpResponseMessage<GetChannelInfoReply>> CreateChannel(NewChannelMessage message)
        {
            request.SetMethod(RestSharp.Method.POST);
            request.SetUrl("channel/create");
            request.AddJToken(message);
            return GetResult<HttpResponseMessage<GetChannelInfoReply>>();
        }

        public Task<HttpResponseMessage<GetChannelInfoReply>> GetChannels(string guildId)
        {
            request.SetMethod(RestSharp.Method.GET);
            request.SetUrl("channel/list");
            request.AddParameter("guild_id", guildId);
            return GetResult<HttpResponseMessage<GetChannelInfoReply>>();
        }

        public Task<HttpResponseMessage<GetServerMemberReply>> GetServerMembers(GetServerMemberMessage message)
        {
            request.SetMethod(RestSharp.Method.GET);
            request.SetUrl("guild/user-list");
            request.AddJToken(message);
            return GetResult<HttpResponseMessage<GetServerMemberReply>>();
        }

        public Task<HttpResponseMessage> RemoveChannel(RemoveChannelMessage message)
        {
            request.SetMethod(RestSharp.Method.POST);
            request.SetUrl("channel/delete");
            request.AddJToken(message);
            return GetResult<HttpResponseMessage>();
        }

        public Task<HttpResponseMessage> RemoveMessage(RemoveMessageMessage message)
        {
            request.SetMethod(RestSharp.Method.POST);
            request.SetUrl("message/delete");
            request.AddJToken(message);
            return GetResult<HttpResponseMessage>();
        }

        public Task<HttpResponseMessage<SendChannelMessageReply>> SendGroupMessage(ChannelMessage message)
        {
            request.SetMethod(RestSharp.Method.POST);
            request.SetUrl("message/create");
            request.AddJToken(message);
            return GetResult<HttpResponseMessage<SendChannelMessageReply>>();
        }

        private async Task<T> GetResult<T>()
        {
            return JObject.Parse((await request.GetResponse()).Content).ToObject<T>();
        }
    }
}
