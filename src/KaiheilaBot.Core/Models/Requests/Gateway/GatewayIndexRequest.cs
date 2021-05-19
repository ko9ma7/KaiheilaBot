using System.Text.Json.Serialization;
using KaiheilaBot.Core.Attributes;
using RestSharp;

namespace KaiheilaBot.Core.Models.Requests.Gateway
{
    /// <summary>
    /// 请求 gateway/index，获取 Websocket 连接地址
    /// </summary>
    public record GatewayIndexRequest : BaseRequest
    {
        public new string ResourcePath { get; init; } = "gateway/index";
        
        public new Method RequestMethod { get; init; } = Method.GET;
        
        [ParameterName("compress")]
        public int Compress { get; set; }
    }
}
