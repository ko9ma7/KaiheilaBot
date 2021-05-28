using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IVideoMessageEventExecutor : IEventExecutor<BaseMessageEvent<VideoMessageEvent>>
    {
        public Task Execute(BaseMessageEvent<VideoMessageEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseMessageEvent<VideoMessageEvent>>.ExecuteInner(BaseMessageEvent<VideoMessageEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
