using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildRelatedEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUpdatedGuildEventExecutor : IEventExecutor<BaseEvent<UpdatedGuildEvent>>
    {
        public Task Execute(BaseEvent<UpdatedGuildEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<UpdatedGuildEvent>>.ExecuteInner(BaseEvent<UpdatedGuildEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
