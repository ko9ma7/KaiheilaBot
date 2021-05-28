using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildRelatedEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IDeletedBlockListEventExecutor : IEventExecutor<BaseEvent<DeletedBlockListEvent>>
    {
        public Task Execute(BaseEvent<DeletedBlockListEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<DeletedBlockListEvent>>.ExecuteInner(BaseEvent<DeletedBlockListEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
