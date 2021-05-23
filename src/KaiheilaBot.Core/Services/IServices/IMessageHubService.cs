using System;
using System.Collections.Generic;
using KaiheilaBot.Core.Extension;

namespace KaiheilaBot.Core.Services.IServices
{
    public interface IMessageHubService
    {
        public void Publish(string type, string message, long sn);

        public void Publish(int type, string message, long sn);

        public List<Guid> Subscribe(IPlugin plugin, IEnumerable<string> required, string pluginUniqueId);

        public void UnSubscribe(string pluginUniqueId);

        public bool CheckSubscribed(string pluginUniqueId);
    }
}