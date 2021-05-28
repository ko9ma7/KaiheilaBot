using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.PrivateMessageEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUpdatedPrivateMessageEventExecutor : IEventExecutor<BaseEvent<UpdatedPrivateMessageEvent>>
    {
        public Task Execute(BaseEvent<UpdatedPrivateMessageEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<UpdatedPrivateMessageEvent>>.ExecuteInner(BaseEvent<UpdatedPrivateMessageEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
