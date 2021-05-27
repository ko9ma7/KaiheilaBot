using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildRoleEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUpdatedRoleEventExecutor : IEventExecutor<BaseEvent<UpdatedRoleEvent>>
    {
        public Task Execute(BaseEvent<UpdatedRoleEvent> e);

        Task IEventExecutor<BaseEvent<UpdatedRoleEvent>>.ExecuteInner(BaseEvent<UpdatedRoleEvent> e)
        {
            return Execute(e);
        }
    }
}
