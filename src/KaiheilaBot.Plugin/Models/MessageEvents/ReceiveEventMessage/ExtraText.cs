using Newtonsoft.Json;

namespace KaiheilaBot
{
    public class ExtraText : Extra
    {
        [JsonProperty("guid_id")]
        public string Guid { get; set; }
        [JsonProperty("channel_name")]
        public string Channel { get; set; }
        [JsonProperty("mention")]
        public object Mention { get; set; }
        [JsonProperty("mention_all")]
        public bool MentionAll { get; set; }
        [JsonProperty("mention_roles")]
        public object MentionRoles { get; set; }
        [JsonProperty("author")]
        public User Author { get; set; }
    }
}
