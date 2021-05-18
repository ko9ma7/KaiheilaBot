using System;
using System.Collections.Generic;
using System.Linq;
using Easy.MessageHub;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Services
{
    // TODO: 日志记录
    public class MessageHubService : IMessageHubService
    {
        private readonly ILogger<MessageHubService> _logger;

        private readonly MessageHub _messageHub = new();
        private readonly Dictionary<Guid, string> _subscribers = new();

        public MessageHubService(ILogger<MessageHubService> logger)
        {
            _logger = logger;
        }
            
        public void Publish<T>(T message)
        {
            _messageHub.Publish(message);
        }

        public Guid Subscribe<T>(Action<T> action, string pluginUniqueId)
        {
            var subGuid = _messageHub.Subscribe(action);
            _subscribers.Add(subGuid, pluginUniqueId);
            return subGuid;
        }
        
        public void UnSubscribe(string pluginUniqueId)
        {
            if (CheckSubscribed(pluginUniqueId) is false)
            {
                return;
            }
            var key = _subscribers.First
                (x => x.Value == pluginUniqueId).Key;
            _messageHub.Unsubscribe(key);
            _subscribers.Remove(key);
        }

        public bool CheckSubscribed(string pluginUniqueId)
        {
            try
            {
                var _ = _subscribers.First
                    (x => x.Value == pluginUniqueId);
                return true;
            }
            catch (InvalidOperationException _)
            {
                return false;
            }
        }
        
    }
}
