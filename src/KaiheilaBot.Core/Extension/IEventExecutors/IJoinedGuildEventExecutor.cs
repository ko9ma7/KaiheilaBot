using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildMemberEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IJoinedGuildEventExecutor : IEventExecutor<BaseEvent<JoinedGuildEvent>>
    {
        public Task Execute(BaseEvent<JoinedGuildEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<JoinedGuildEvent>>.ExecuteInner(BaseEvent<JoinedGuildEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
