using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildMemberEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IJoinedGuildEventExecutor : IEventExecutor<BaseEvent<JoinedGuildEvent>>
    {
        public Task Execute(BaseEvent<JoinedGuildEvent> e);

        Task IEventExecutor<BaseEvent<JoinedGuildEvent>>.ExecuteInner(BaseEvent<JoinedGuildEvent> e)
        {
            return Execute(e);
        }
    }
}
