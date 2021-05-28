using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildRoleEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUpdatedRoleEventExecutor : IEventExecutor<BaseEvent<UpdatedRoleEvent>>
    {
        public Task Execute(BaseEvent<UpdatedRoleEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<UpdatedRoleEvent>>.ExecuteInner(BaseEvent<UpdatedRoleEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
