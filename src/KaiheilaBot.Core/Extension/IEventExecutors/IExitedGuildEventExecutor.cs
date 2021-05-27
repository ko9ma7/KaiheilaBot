using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildMemberEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IExitedGuildEventExecutor : IEventExecutor<BaseEvent<ExitedGuildEvent>>
    {
        public Task Execute(BaseEvent<ExitedGuildEvent> e);

        Task IEventExecutor<BaseEvent<ExitedGuildEvent>>.ExecuteInner(BaseEvent<ExitedGuildEvent> e)
        {
            return Execute(e);
        }
    }
}
