using Easy.MessageHub;

namespace KaiheilaBot.Models
{
    public class Shared
    {
        public MessageHub messageHub { get; internal set; } = new MessageHub();
    }
}
