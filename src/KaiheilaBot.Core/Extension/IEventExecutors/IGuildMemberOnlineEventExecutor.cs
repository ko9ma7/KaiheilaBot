using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildMemberEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IGuildMemberOnlineEventExecutor : IEventExecutor<BaseEvent<GuildMemberOnlineEvent>>
    {
        public Task Execute(BaseEvent<GuildMemberOnlineEvent> e);

        Task IEventExecutor<BaseEvent<GuildMemberOnlineEvent>>.ExecuteInner(BaseEvent<GuildMemberOnlineEvent> e)
        {
            return Execute(e);
        }
    }
}
