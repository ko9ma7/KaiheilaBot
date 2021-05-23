using System;

namespace KaiheilaBot.Core.Services.IServices
{
    public interface IMessageHubService
    {
        public void Publish(string type, string message, long sn);

        public void Publish(int type, string message, long sn);

        public Guid Subscribe<T>(Action<T> action, string pluginUniqueId);

        public void UnSubscribe(string pluginUniqueId);

        public bool CheckSubscribed(string pluginUniqueId);
    }
}