using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildMemberEvents;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IGuildMemberOfflineEventExecutor : IEventExecutor<BaseEvent<GuildMemberOfflineEvent>>
    {
        public Task Execute(BaseEvent<GuildMemberOfflineEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<GuildMemberOfflineEvent>>.ExecuteInner(BaseEvent<GuildMemberOfflineEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
