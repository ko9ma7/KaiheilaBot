using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IExitedChannelEventExecutor : IEventExecutor<BaseEvent<ExitedChannelEvent>>
    {
        public Task Execute(BaseEvent<ExitedChannelEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<ExitedChannelEvent>>.ExecuteInner(BaseEvent<ExitedChannelEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
