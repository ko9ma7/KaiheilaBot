using KaiheilaBot.Core.Models.Objects;

namespace KaiheilaBot.Core.Models.Events.ChannelRelatedEvents
{
    public record AddedChannelEvent : Channel, IBaseEventExtraBody
    {
        
    }
}