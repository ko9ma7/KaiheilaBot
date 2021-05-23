using System;
using System.Collections.Generic;
using System.Linq;
using Easy.MessageHub;
using KaiheilaBot.Core.Common.Serializers;
using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using KaiheilaBot.Core.Models.Events.GuildMemberEvents;
using KaiheilaBot.Core.Models.Events.GuildRelatedEvents;
using KaiheilaBot.Core.Models.Events.GuildRoleEvents;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using KaiheilaBot.Core.Models.Events.PrivateMessageEvents;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Services
{
    public class MessageHubService : IMessageHubService
    {
        private readonly ILogger<MessageHubService> _logger;

        private readonly MessageHub _messageHub = new();
        private readonly Dictionary<Guid, string> _subscribers = new();

        public MessageHubService(ILogger<MessageHubService> logger)
        {
            _logger = logger;
            
            _logger.LogDebug("注册 MessageHub 全局消息 Handler");
            _messageHub.RegisterGlobalHandler((type, obj) =>
            {
                _logger.LogDebug($"MessageHub 已成功添加: {type}, 内容: {obj}");
            });
            
            _logger.LogDebug("注册 MessageHub 全局 Error Handler");
            _messageHub.RegisterGlobalErrorHandler((token, e) =>
            {
                _logger.LogWarning($"MessageHub 出现错误，Key: {token} | 错误: {e}");
            });
        }

        /// <summary>
        /// 发布消息（非 MessageRelatedEvent）
        /// </summary>
        /// <param name="type">消息类型字符串</param>
        /// <param name="message">完整的消息 Json 字符串</param>
        /// <param name="sn">消息 sn 编号</param>
        public void Publish(string type, string message, long sn)
        {
            // 可能存在优化方法，复杂度 O(1)，最坏 O(n)
            // https://stackoverflow.com/questions/4442835/what-is-the-runtime-complexity-of-a-switch-statement
            // 已验证：在此种设计下，除了一个个判断，使用反射无法获得具体的类型
            _logger.LogDebug($"MessageHub Publish 收到的类型字符串：{type}");
            _logger.LogDebug($"MessageHub 收到的消息体: {message}");
            switch (type)
            {
                // UserRelatedEvents
                case "ExitedChannelEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<ExitedChannelEvent>(message), sn);
                    break;
                case "JoinedChannelEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<JoinedChannelEvent>(message), sn);
                    break;
                case "MessageBtnClickEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<MessageBtnClickEvent>(message), sn);
                    break;
                case "SelfExitedGuildEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<SelfExitedGuildEvent>(message), sn);
                    break;
                case "SelfJoinedGuildEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<SelfJoinedGuildEvent>(message), sn);
                    break;
                case "UserUpdatedEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<UserUpdatedEvent>(message), sn);
                    break;
                
                // ChannelRelatedEvent
                case "AddedChannelEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<AddedChannelEvent>(message), sn);
                    break;
                case "AddedReactionEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<AddedReactionEvent>(message), sn);
                    break;
                case "DeletedChannelEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<DeletedChannelEvent>(message), sn);
                    break;
                case "DeletedMessageEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<DeletedMessageEvent>(message), sn);
                    break;
                case "DeletedReactionEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<DeletedReactionEvent>(message), sn);
                    break;
                case "PinnedMessageEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<PinnedMessageEvent>(message), sn);
                    break;
                case "UnpinnedMessageEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<UnpinnedMessageEvent>(message), sn);
                    break;
                case "UpdatedChannelEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<UpdatedChannelEvent>(message), sn);
                    break;
                case "UpdatedMessageEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<UpdatedMessageEvent>(message), sn);
                    break;
            
                // GuildMemberEvents
                case "ExitedGuildEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<ExitedGuildEvent>(message), sn);
                    break;
                case "GuildMemberOfflineEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<GuildMemberOfflineEvent>(message), sn);
                    break;
                case "GuildMemberOnlineEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<GuildMemberOnlineEvent>(message), sn);
                    break;
                case "JoinedGuildEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<JoinedGuildEvent>(message), sn);
                    break;
                case "UpdatedGuildMemberEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<UpdatedGuildMemberEvent>(message), sn);
                    break;

                // GuildRoleEvents
                case "AddedRoleEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<AddedRoleEvent>(message), sn);
                    break;
                case "DeletedRoleEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<DeletedRoleEvent>(message), sn);
                    break;
                case "UpdatedRoleEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<UpdatedRoleEvent>(message), sn);
                    break;
            
                // PrivateMessageEvents
                case "DeletedPrivateMessageEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<DeletedPrivateMessageEvent>(message), sn);
                    break;
                case "PrivateAddedReactionEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<PrivateAddedReactionEvent>(message), sn);
                    break;
                case "PrivateDeletedReactionEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<PrivateDeletedReactionEvent>(message), sn);
                    break;
                case "UpdatedPrivateMessageEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<UpdatedPrivateMessageEvent>(message), sn);
                    break;
                
                // GuildRelatedEvents
                case "AddedBlockListEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<AddedBlockListEvent>(message), sn);
                    break;
                case "DeletedBlockListEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<DeletedBlockListEvent>(message), sn);
                    break;
                case "DeletedGuildEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<DeletedGuildEvent>(message), sn);
                    break;
                case "UpdatedGuildEvent":
                    PublishBaseEvent(EventSerializer.DeserializeEvent<UpdatedGuildEvent>(message), sn);
                    break;
            }
        }

        /// <summary>
        /// 发布消息（MessageRelatedEvent）
        /// </summary>
        /// <param name="type">消息类型标识</param>
        /// <param name="message">完整的消息 Json 字符串</param>
        /// <param name="sn">消息 sn 编号</param>
        public void Publish(int type, string message, long sn)
        {
            _logger.LogDebug($"MessageHub Publish 收到的类型整数：{type}");
            _logger.LogDebug($"MessageHub 收到的消息体: {message}");
            switch (type)
            {
                case 1:
                    PublishMessageEvent(EventSerializer.DeserializeMessageEvent<TextMessageEvent>(message), sn);
                    break;
                case 2:
                    PublishMessageEvent(EventSerializer.DeserializeMessageEvent<ImageMessageEvent>(message), sn);
                    break;
                case 3:
                    PublishMessageEvent(EventSerializer.DeserializeMessageEvent<VideoMessageEvent>(message), sn);
                    break;
                case 4:
                    PublishMessageEvent(EventSerializer.DeserializeMessageEvent<FileMessageEvent>(message), sn);
                    break;
                case 9:
                    PublishMessageEvent(EventSerializer.DeserializeMessageEvent<KmarkdownMessageEvent>(message), sn);
                    break;
                case 10:
                    PublishMessageEvent(EventSerializer.DeserializeMessageEvent<CardMessageEvent>(message), sn);
                    break;
            }
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="action">消息处理 Action</param>
        /// <param name="pluginUniqueId">插件唯一 ID</param>
        /// <typeparam name="T">订阅的消息类型</typeparam>
        /// <returns></returns>
        public Guid Subscribe<T>(Action<T> action, string pluginUniqueId)
        {
            var subGuid = _messageHub.Subscribe(action);
            _subscribers.Add(subGuid, pluginUniqueId);
            return subGuid;
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="pluginUniqueId">插件唯一 ID</param>
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

        /// <summary>
        /// 确认是否存在订阅
        /// </summary>
        /// <param name="pluginUniqueId">插件唯一 ID</param>
        /// <returns></returns>
        public bool CheckSubscribed(string pluginUniqueId)
        {
            try
            {
                var _ = _subscribers.First
                    (x => x.Value == pluginUniqueId);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        
        private void PublishBaseEvent<T>(BaseEvent<T> baseEvent, long sn) where T : IBaseEventExtraBody
        {
            if (baseEvent is null)
            {
                _logger.LogWarning($"反序列化 {typeof(T)} 类型消息失败");
                return;
            }
            _messageHub.Publish(baseEvent);
            _logger.LogInformation($"已成功发布类型为 {typeof(T)} 的消息至 MessageHub，Sn = {sn}");
        }

        private void PublishMessageEvent<T>(BaseMessageEvent<T> messageEvent, long sn) where T : IBaseMessageEventDataExtra
        {
            if (messageEvent is null)
            {
                _logger.LogWarning($"反序列化 {typeof(T)} 类型消息失败");
                return;
            }
            _messageHub.Publish(messageEvent);
            _logger.LogInformation($"已成功发布类型为 {typeof(T)} 的消息至 MessageHub，Sn = {sn}");
        }
    }
}