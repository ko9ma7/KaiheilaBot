using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUpdatedGuildEventExecutor : IEventExecutor<BaseEvent<UpdatedGuildEvent>>
    {
        public Task Execute(BaseEvent<UpdatedGuildEvent> e);

        Task IEventExecutor<BaseEvent<UpdatedGuildEvent>>.ExecuteInner(BaseEvent<UpdatedGuildEvent> e)
        {
            return Execute(e);
        }
    }
}
