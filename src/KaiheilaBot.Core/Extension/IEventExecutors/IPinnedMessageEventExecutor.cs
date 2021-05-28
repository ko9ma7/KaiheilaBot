using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IPinnedMessageEventExecutor : IEventExecutor<BaseEvent<PinnedMessageEvent>>
    {
        public Task Execute(BaseEvent<PinnedMessageEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<PinnedMessageEvent>>.ExecuteInner(BaseEvent<PinnedMessageEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
