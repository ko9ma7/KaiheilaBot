using KaiheilaBot.Core.Attributes;
using RestSharp;

namespace KaiheilaBot.Core.Models.Requests.Guild
{
    public record DeleteMuteGuildMemberRequest : BaseRequest
    {
        public override Method RequestMethod { get; init; } = Method.POST;
        
        public override string ResourcePath { get; init; } = "guild-mute/delete";

        [ParameterName("guild_id")]
        public string GuildId { get; set; }

        [ParameterName("user_id")]
        public string UserId { get; set; }

        [ParameterName("type")]
        public string Type { get; set; }
    }
}