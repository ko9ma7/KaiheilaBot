using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Responses.Guild
{
    public record GetGuildMuteListDetailResponse : IBaseResponseData
    {
        [JsonPropertyName("mic")]
        public GuildMuteMicList MuteMicList { get; set; }

        [JsonPropertyName("headset")]
        public GuildMuteHeadsetList MuteHeadsetList { get; set; }
    }

    public record GuildMuteMicList
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("user_ids")]
        public IEnumerable<string> UserIds { get; set; }
    }

    public record GuildMuteHeadsetList
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("user_ids")]
        public IEnumerable<string> UserIds { get; set; }
    }
}