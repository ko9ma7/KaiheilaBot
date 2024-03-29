﻿using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.PrivateMessageEvents;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IPrivateAddedReactionEventExecutor : IEventExecutor<BaseEvent<PrivateAddedReactionEvent>>
    {
        public Task Execute(BaseEvent<PrivateAddedReactionEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<PrivateAddedReactionEvent>>.ExecuteInner(BaseEvent<PrivateAddedReactionEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
