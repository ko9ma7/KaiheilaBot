using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IAddedBlockListEventExecutor : IEventExecutor<BaseEvent<AddedBlockListEvent>>
    {
        public Task Execute(BaseEvent<AddedBlockListEvent> e);

        Task IEventExecutor<BaseEvent<AddedBlockListEvent>>.ExecuteInner(BaseEvent<AddedBlockListEvent> e)
        {
            return Execute(e);
        }
    }
}
