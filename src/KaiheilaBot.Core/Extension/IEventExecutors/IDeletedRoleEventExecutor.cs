using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.GuildRoleEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IDeletedRoleEventExecutor : IEventExecutor<BaseEvent<DeletedRoleEvent>>
    {
        public Task Execute(BaseEvent<DeletedRoleEvent> e);

        Task IEventExecutor<BaseEvent<DeletedRoleEvent>>.ExecuteInner(BaseEvent<DeletedRoleEvent> e)
        {
            return Execute(e);
        }
    }
}
