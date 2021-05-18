using System;

namespace KaiheilaBot.Core.Services.IServices
{
    public interface IMessageHubService
    {
        public void Publish<T>(T message);

        public Guid Subscribe<T>(Action<T> action, string pluginUniqueId);

        public void UnSubscribe(string pluginUniqueId);

        public bool CheckSubscribed(string pluginUniqueId);
    }
}