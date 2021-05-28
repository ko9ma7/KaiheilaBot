using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildMemberEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IGuildMemberOnlineEventExecutor : IEventExecutor<BaseEvent<GuildMemberOnlineEvent>>
    {
        public Task Execute(BaseEvent<GuildMemberOnlineEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<GuildMemberOnlineEvent>>.ExecuteInner(BaseEvent<GuildMemberOnlineEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
