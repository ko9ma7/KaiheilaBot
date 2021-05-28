using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUpdatedChannelEventExecutor : IEventExecutor<BaseEvent<UpdatedChannelEvent>>
    {
        public Task Execute(BaseEvent<UpdatedChannelEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<UpdatedChannelEvent>>.ExecuteInner(BaseEvent<UpdatedChannelEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
