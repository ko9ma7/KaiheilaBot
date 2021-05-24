using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KaiheilaBot.Core.Models.Responses.Guild
{
    public record GetGuildMuteListResponse : IBaseResponseData
    {
        [JsonPropertyName("1")]
        public IEnumerable<string> MuteMicList { get; set; }

        [JsonPropertyName("2")]
        public IEnumerable<string> MuteHeadsetList { get; set; }
    }
}
