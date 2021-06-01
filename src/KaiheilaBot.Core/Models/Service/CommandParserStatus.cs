namespace KaiheilaBot.Core.Models.Service
{
    public enum CommandParserStatus
    {
        Success,
        NoMatchCommand,
        NoPermission,
        ChannelNotAllowed,
        FunctionError
    }
}
