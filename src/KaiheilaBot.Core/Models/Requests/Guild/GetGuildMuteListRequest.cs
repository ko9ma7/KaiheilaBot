using System.Text.Json.Serialization;
using RestSharp;

namespace KaiheilaBot.Core.Models.Requests.Guild
{
    public record GetGuildMuteListRequest : BaseRequest
    {
        public override Method RequestMethod { get; init; } = Method.GET;

        public override string ResourcePath { get; init; } = "guild-mute/list";

        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; }

        /// <summary>
        /// 此值若为 detail，返回类型为 GetGuildMuteListDetailResponse，
        /// 否则返回的为 GetGuildMuteListResponse
        /// </summary>
        [JsonPropertyName("return_type")]
        public string ReturnType { get; set; }
    }
}