using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildMemberEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUpdatedGuildMemberEventExecutor : IEventExecutor<BaseEvent<UpdatedGuildMemberEvent>>
    {
        public Task Execute(BaseEvent<UpdatedGuildMemberEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<UpdatedGuildMemberEvent>>.ExecuteInner(BaseEvent<UpdatedGuildMemberEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
