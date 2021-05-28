using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface ITextMessageEventExecutor : IEventExecutor<BaseMessageEvent<TextMessageEvent>>
    {
        public Task Execute(BaseMessageEvent<TextMessageEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseMessageEvent<TextMessageEvent>>.ExecuteInner(BaseMessageEvent<TextMessageEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
