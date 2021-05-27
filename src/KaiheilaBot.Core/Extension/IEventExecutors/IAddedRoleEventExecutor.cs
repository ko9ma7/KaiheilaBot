using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildRoleEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IAddedRoleEventExecutor : IEventExecutor<BaseEvent<AddedRoleEvent>>
    {
        public Task Execute(BaseEvent<AddedRoleEvent> e);

        Task IEventExecutor<BaseEvent<AddedRoleEvent>>.ExecuteInner(BaseEvent<AddedRoleEvent> e)
        {
            return Execute(e);
        }
    }
}
