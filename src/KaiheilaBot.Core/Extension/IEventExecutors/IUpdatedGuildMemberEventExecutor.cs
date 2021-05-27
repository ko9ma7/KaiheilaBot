using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildMemberEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUpdatedGuildMemberEventExecutor : IEventExecutor<BaseEvent<UpdatedGuildMemberEvent>>
    {
        public Task Execute(BaseEvent<UpdatedGuildMemberEvent> e);

        Task IEventExecutor<BaseEvent<UpdatedGuildMemberEvent>>.ExecuteInner(BaseEvent<UpdatedGuildMemberEvent> e)
        {
            return Execute(e);
        }
    }
}
