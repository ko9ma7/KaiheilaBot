
namespace KaiheilaBot
{
    public class GetChannelMessage:AbstractMessageType
    {
        public GetChannelMessage(string guild_id)
        {
            this.guild_id = guild_id;
        }
        public string guild_id { get; set; }
    }
}
