using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IDeletedChannelEventExecutor : IEventExecutor<BaseEvent<DeletedChannelEvent>>
    {
        public Task Execute(BaseEvent<DeletedChannelEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<DeletedChannelEvent>>.ExecuteInner(BaseEvent<DeletedChannelEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
