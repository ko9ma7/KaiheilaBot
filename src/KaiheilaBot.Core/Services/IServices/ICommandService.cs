using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using KaiheilaBot.Core.Models.Service;

namespace KaiheilaBot.Core.Services.IServices
{
    public interface ICommandService
    {
        public void AddCommand(CommandNode rootCommandNode);

        public CommandParserStatus Parse(BaseMessageEvent<TextMessageEvent> e);
    }
}
