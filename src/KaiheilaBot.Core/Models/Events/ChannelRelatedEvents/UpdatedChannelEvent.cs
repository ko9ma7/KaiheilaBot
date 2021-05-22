using KaiheilaBot.Core.Models.Objects;

namespace KaiheilaBot.Core.Models.Events.ChannelRelatedEvents
{
    public record UpdatedChannelEvent : Channel, IBaseEventExtraBody
    {
        
    }
}