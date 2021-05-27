using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IFileMessageEventExecutor : IEventExecutor<BaseMessageEvent<FileMessageEvent>>
    {
        public Task Execute(BaseMessageEvent<FileMessageEvent> e);

        Task IEventExecutor<BaseMessageEvent<FileMessageEvent>>.ExecuteInner(BaseMessageEvent<FileMessageEvent> e)
        {
            return Execute(e);
        }
    }
}
