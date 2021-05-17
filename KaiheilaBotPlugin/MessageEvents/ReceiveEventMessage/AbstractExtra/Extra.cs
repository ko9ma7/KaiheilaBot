using Newtonsoft.Json;

namespace KaiheilaBot
{
    [JsonConverter(typeof(ExtraConverter))]
    public abstract class Extra
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
