using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Easy.MessageHub;
using KaiheilaBot.Core.Common.Serializers;
using KaiheilaBot.Core.Extension;
using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using KaiheilaBot.Core.Models.Events.GuildMemberEvents;
using KaiheilaBot.Core.Models.Events.GuildRelatedEvents;
using KaiheilaBot.Core.Models.Events.GuildRoleEvents;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using KaiheilaBot.Core.Models.Events.PrivateMessageEvents;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using KaiheilaBot.Core.Models.Service;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Services
{
    public class MessageHubService : IMessageHubService
    {
        private readonly ILogger<MessageHubService> _logger;
        private readonly IServiceProvider _serviceProvider;

        private readonly MessageHub _messageHub = new();
        private readonly Dictionary<string, List<Guid>> _subscribers = new();

        public MessageHubService(ILogger<MessageHubService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            _logger.LogDebug("MH - 注册 MessageHub 全局消息 Handler");
            _messageHub.RegisterGlobalHandler((type, obj) =>
            {
                _logger.LogDebug($"MH - MessageHub 已成功添加: {type}, 内容: {obj}");
            });
            
            _logger.LogDebug("MH - 注册 MessageHub 全局 Error Handler");
            _messageHub.RegisterGlobalErrorHandler((token, e) =>
            {
                _logger.LogWarning($"MH - MessageHub 出现错误，Key: {token} | 错误: {e}");
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
            // 可能存在优化方法，复杂度 O(1)，最坏 O(LogN)
            // https://stackoverflow.com/questions/4442835/what-is-the-runtime-complexity-of-a-switch-statement
            // 存在格式优化可能，参考：
            // https://stackoverflow.com/questions/67655790/is-it-possible-to-optimize-large-switch-statements-in-c
            _logger.LogDebug($"MH - MessageHub Publish 收到的类型字符串：{type}");
            _logger.LogDebug($"MH - MessageHub 收到的消息体: {message}");
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
            _logger.LogDebug($"MH - MessageHub Publish 收到的类型整数：{type}");
            _logger.LogDebug($"MH - MessageHub 收到的消息体: {message}");
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
        /// 发布消息（由 HttpServer 接受的消息）
        /// </summary>
        /// <param name="pluginId">插件 ID</param>
        /// <param name="data">消息内容</param>
        public void Publish(string pluginId, string data)
        {
            var dataPacked = new HttpServerData()
            {
                PluginId = pluginId,
                Data = data
            };
            _messageHub.Publish(dataPacked);
        }
        
        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="pluginInfo">插件信息</param>
        /// <returns></returns>
        public List<Guid> Subscribe(PluginInfo pluginInfo)
        {
            var guidList = new List<Guid>();
            foreach (var executor in pluginInfo.GetExecutors())
            {
                var type = executor.TypeString;
                switch (type)
                {
                    // MessageRelatedEvents
                    case "ITextMessageEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseMessageEvent<TextMessageEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IImageMessageEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseMessageEvent<ImageMessageEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IVideoMessageEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseMessageEvent<VideoMessageEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IFileMessageEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseMessageEvent<FileMessageEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IKmarkdownMessageEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseMessageEvent<KmarkdownMessageEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "ICardMessageEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseMessageEvent<CardMessageEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;

                    // UserRelatedEvents
                    case "IExitedChannelEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<ExitedChannelEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IJoinedChannelEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<JoinedChannelEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IMessageBtnClickEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<MessageBtnClickEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "ISelfExitedGuildEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<SelfExitedGuildEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "ISelfJoinedGuildEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<SelfJoinedGuildEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IUserUpdatedEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<UserUpdatedEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    
                    // ChannelRelatedEvent
                    case "IAddedChannelEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<AddedChannelEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IAddedReactionEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<AddedReactionEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IDeletedChannelEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<DeletedChannelEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IDeletedMessageEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<DeletedMessageEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IDeletedReactionEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<DeletedReactionEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IPinnedMessageEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<PinnedMessageEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IUnpinnedMessageEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<UnpinnedMessageEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IUpdatedChannelEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<UpdatedChannelEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IUpdatedMessageEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<UpdatedMessageEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                
                    // GuildMemberEvents
                    case "IExitedGuildEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<ExitedGuildEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IGuildMemberOfflineEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<GuildMemberOfflineEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IGuildMemberOnlineEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<GuildMemberOnlineEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IJoinedGuildEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<JoinedGuildEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IUpdatedGuildMemberEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<UpdatedGuildMemberEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;

                    // GuildRoleEvents
                    case "IAddedRoleEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<AddedRoleEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IDeletedRoleEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<DeletedRoleEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IUpdatedRoleEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<UpdatedRoleEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                
                    // PrivateMessageEvents
                    case "IDeletedPrivateMessageEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<DeletedPrivateMessageEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IPrivateAddedReactionEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<PrivateAddedReactionEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IPrivateDeletedReactionEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<PrivateDeletedReactionEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IUpdatedPrivateMessageEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<UpdatedPrivateMessageEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    
                    // GuildRelatedEvents
                    case "IAddedBlockListEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<AddedBlockListEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IDeletedBlockListEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<DeletedBlockListEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IDeletedGuildEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<DeletedGuildEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    case "IUpdatedGuildEventExecutor":
                        guidList.Add(_messageHub.Subscribe<BaseEvent<UpdatedGuildEvent>>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data, 
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                    
                    // HttpServerDataResolver
                    case "HttpServerData":
                        guidList.Add(_messageHub.Subscribe<HttpServerData>
                            (async data => await ExecuteAction(pluginInfo.GetId(), data,
                                executor.Method, executor.ExecutorClassInstance)));
                        break;
                }
            }
            _subscribers.Add(pluginInfo.GetId(), guidList);
            _logger.LogInformation($"MH - 成功注册插件 {pluginInfo.GetId()} 的订阅事件，共 {guidList.Count} 个");
            return guidList;
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="pluginUniqueId">插件唯一 ID</param>
        public bool UnSubscribe(string pluginUniqueId)
        {
            if (CheckSubscribed(pluginUniqueId) is false)
            {
                return false;
            }

            var guids = _subscribers.First
                (x => x.Key == pluginUniqueId).Value;
            foreach (var guid in guids)
            {
                _messageHub.Unsubscribe(guid);
            }
            _subscribers.Remove(pluginUniqueId);
            return true;
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
                    (x => x.Key == pluginUniqueId);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        /// <summary>
        /// 卸载 MessageHub
        /// </summary>
        public void Dispose()
        {
            _messageHub.Dispose();
        }

        private void PublishBaseEvent<T>(BaseEvent<T> baseEvent, long sn) where T : IBaseEventExtraBody
        {
            if (baseEvent is null)
            {
                _logger.LogWarning($"MH - 反序列化 {typeof(T)} 类型消息失败");
                return;
            }
            _messageHub.Publish(baseEvent);
            _logger.LogInformation($"MH - 已成功发布类型为 {typeof(T)} 的消息至 MessageHub，Sn = {sn}");
        }

        private void PublishMessageEvent<T>(BaseMessageEvent<T> messageEvent, long sn) where T : IBaseMessageEventDataExtra
        {
            if (messageEvent is null)
            {
                _logger.LogWarning($"MH - 反序列化 {typeof(T)} 类型消息失败");
                return;
            }
            _messageHub.Publish(messageEvent);
            _logger.LogInformation($"MH - 已成功发布类型为 {typeof(T)} 的消息至 MessageHub，Sn = {sn}");
        }

        private async Task ExecuteAction<T>(string id, T data, MethodBase method, object instance)
        {
            await Task.Delay(500);  // 1.规避 API 速率限制    2.等待 Logger 响应
            var sw = new Stopwatch();
            sw.Start();
            // ReSharper disable once PossibleNullReferenceException
            await (Task) method.Invoke(instance, new object[]
            {
                data,
                _serviceProvider.GetService<ILogger<IPlugin>>(),
                _serviceProvider.GetService<IHttpApiRequestService>()
            });
            sw.Stop();

            var dataType = typeof(T) == typeof(HttpServerData) ? 
                "HttpServerData" : data.GetType().ToString().Split('1')[1];
            _logger.LogInformation(
                $"MH - {id} 插件处理 " +
                $"{dataType} 类型信息完成，" +
                $"耗时 {sw.ElapsedMilliseconds} 毫秒");
        }
    }
}
