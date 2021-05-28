using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.PrivateMessageEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IPrivateDeletedReactionEventExecutor : IEventExecutor<BaseEvent<PrivateDeletedReactionEvent>>
    {
        public Task Execute(BaseEvent<PrivateDeletedReactionEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<PrivateDeletedReactionEvent>>.ExecuteInner(BaseEvent<PrivateDeletedReactionEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
