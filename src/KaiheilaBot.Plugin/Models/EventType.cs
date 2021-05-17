
namespace KaiheilaBot
{
    /// <summary>
    /// 消息处理类别
    /// </summary>
    public enum EventType
    {
        ChannelTextMessage,
        MemberJoinChannel,
        MemberLeaveChannel,
        MemberChangeNick,
        ChannelCreated,
        ChannelEdited,
        ChannelRemoved,
        BotInvited,
        MessageChanged,
        MessageRemoved,
        MessageReacted,
        MessagePinned
    }
}
