using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildMemberEvents;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IGuildMemberOfflineEventExecutor : IEventExecutor<BaseEvent<GuildMemberOfflineEvent>>
    {
        public Task Execute(BaseEvent<GuildMemberOfflineEvent> e);

        Task IEventExecutor<BaseEvent<GuildMemberOfflineEvent>>.ExecuteInner(BaseEvent<GuildMemberOfflineEvent> e)
        {
            return Execute(e);
        }
    }
}
