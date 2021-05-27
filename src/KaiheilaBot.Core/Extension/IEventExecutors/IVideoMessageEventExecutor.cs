using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IVideoMessageEventExecutor : IEventExecutor<BaseMessageEvent<VideoMessageEvent>>
    {
        public Task Execute(BaseMessageEvent<VideoMessageEvent> e);

        Task IEventExecutor<BaseMessageEvent<VideoMessageEvent>>.ExecuteInner(BaseMessageEvent<VideoMessageEvent> e)
        {
            return Execute(e);
        }
    }
}
