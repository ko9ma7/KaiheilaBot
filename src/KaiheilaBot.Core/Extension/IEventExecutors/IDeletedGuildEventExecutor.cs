using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IDeletedGuildEventExecutor : IEventExecutor<BaseEvent<DeletedGuildEvent>>
    {
        public Task Execute(BaseEvent<DeletedGuildEvent> e);

        Task IEventExecutor<BaseEvent<DeletedGuildEvent>>.ExecuteInner(BaseEvent<DeletedGuildEvent> e)
        {
            return Execute(e);
        }
    }
}
