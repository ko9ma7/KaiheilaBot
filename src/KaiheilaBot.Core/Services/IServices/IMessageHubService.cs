using KaiheilaBot.Core.Extension;
using System;
using System.Collections.Generic;

namespace KaiheilaBot.Core.Services.IServices
{
    public interface IMessageHubService
    {
        public void Publish(string type, string message, long sn);

        public void Publish(int type, string message, long sn);

        public void Publish(string pluginId, string message);
        
        public List<Guid> Subscribe(PluginInfo pluginInfo);

        public bool UnSubscribe(string pluginUniqueId);

        public bool CheckSubscribed(string pluginUniqueId);

        public void Dispose();
    }
}