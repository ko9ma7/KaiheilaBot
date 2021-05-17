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

        public Task<JObject> ChangeNick(ChangeNickMessage message)
        {
            throw new System.NotImplementedException();
        }

        public Task<JObject> CreateChannel(NewChannelMessage message)
        {
            throw new System.NotImplementedException();
        }

        public Task<JObject> GetChannels()
        {
            throw new System.NotImplementedException();
        }

        public Task<JObject> GetServerMembers(GetServerMemberMessage message)
        {
            throw new System.NotImplementedException();
        }

        public Task<JObject> RemoveChannel(RemoveChannelMessage message)
        {
            throw new System.NotImplementedException();
        }

        public Task<JObject> RemoveMessage(RemoveMessageMessage message)
        {
            throw new System.NotImplementedException();
        }

        public Task<JObject> SendGroupMessage(ChannelMessage message)
        {
            request.SetMethod(RestSharp.Method.POST);
            request.SetUrl("message/create");
            request.AddJToken(message);
            return GetResult();
        }

        private async Task<JObject> GetResult()
        {
            return JObject.Parse((await request.GetResponse()).Content);
        }
    }
}
