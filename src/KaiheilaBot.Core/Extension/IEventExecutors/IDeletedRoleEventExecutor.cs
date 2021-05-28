using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildRoleEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IDeletedRoleEventExecutor : IEventExecutor<BaseEvent<DeletedRoleEvent>>
    {
        public Task Execute(BaseEvent<DeletedRoleEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<DeletedRoleEvent>>.ExecuteInner(BaseEvent<DeletedRoleEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
