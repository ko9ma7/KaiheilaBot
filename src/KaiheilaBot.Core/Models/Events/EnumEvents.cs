namespace KaiheilaBot.Core.Models.Events
{
    public enum EnumEvents
    {
        // Channel Related Events
        AddedChannelEvent,
        AddedReactionEvent,
        DeletedChannelEvent,
        DeletedMessageEvent,
        DeletedReactionEvent,
        PinnedMessageEvent,
        UnpinnedMessageEvent,
        UpdatedChannelEvent,
        UpdatedMessageEvent,

        // Guild Member Events
        ExitedGuildEvent,
        GuildMemberOfflineEvent,
        GuildMemberOnlineEvent,
        JoinedGuildEvent,
        UpdatedGuildMemberEvent,

        // Guild Related Events
        AddedBlockListEvent,
        DeletedBlockListEvent,
        DeletedGuildEvent,
        UpdatedGuildEvent,

        // Guild Role Events
        AddedRoleEvent,
        DeletedRoleEvent,
        UpdatedRoleEvent,

        // Message Related Events
        CardMessageEvent,
        FileMessageEvent,
        ImageMessageEvent,
        KmarkdownMessageEvent,
        TextMessageEvent,
        VideoMessageEvent,

        // Private Message Events
        DeletedPrivateMessageEvent,
        PrivateAddedReactionEvent,
        PrivateDeletedReactionEvent,
        UpdatedPrivateMessageEvent,

        // User Related Events
        ExitedChannelEvent,
        JoinedChannelEvent,
        MessageBtnClickEvent,
        SelfExitedGuildEvent,
        SelfJoinedGuildEvent,
        UserUpdatedEvent
    }
}
