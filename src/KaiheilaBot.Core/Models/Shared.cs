using Easy.MessageHub;
using System.Collections.Generic;

namespace KaiheilaBot.Models
{
    public class Shared
    {
        public MessageHub messageHub { get; internal set; } = new MessageHub();

        public Dictionary<string, object> Cache { get; internal set; } = new Dictionary<string, object>();
    }
}
