using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IDeletedBlockListEventExecutor : IEventExecutor<BaseEvent<DeletedBlockListEvent>>
    {
        public Task Execute(BaseEvent<DeletedBlockListEvent> e);

        Task IEventExecutor<BaseEvent<DeletedBlockListEvent>>.ExecuteInner(BaseEvent<DeletedBlockListEvent> e)
        {
            return Execute(e);
        }
    }
}
