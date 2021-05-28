using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildRoleEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IAddedRoleEventExecutor : IEventExecutor<BaseEvent<AddedRoleEvent>>
    {
        public Task Execute(BaseEvent<AddedRoleEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<AddedRoleEvent>>.ExecuteInner(BaseEvent<AddedRoleEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
