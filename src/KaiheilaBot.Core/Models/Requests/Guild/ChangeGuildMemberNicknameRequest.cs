using KaiheilaBot.Core.Attributes;
using RestSharp;

namespace KaiheilaBot.Core.Models.Requests.Guild
{
    public record ChangeGuildMemberNicknameRequest : BaseRequest
    {
        public override Method RequestMethod { get; init; } = Method.POST;

        public override string ResourcePath { get; init; } = "guild/nickname";

        [ParameterName("guild_id")]
        public string GuildId { get; set; }

        [ParameterName("nickname")]
        public string Nickname { get; set; }

        [ParameterName("user_id")]
        public string UserId { get; set; }
    }
}
