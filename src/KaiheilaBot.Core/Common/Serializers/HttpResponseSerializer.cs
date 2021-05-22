using System.Text.Json;
using KaiheilaBot.Core.Models.Responses;

namespace KaiheilaBot.Core.Common.Serializers
{
    public static class HttpResponseSerializer
    {
        /// <summary>
        /// 反序列化 RestResponse.Content Json 字符串
        /// </summary>
        /// <param name="jsonString">Json 字符串</param>
        /// <typeparam name="T">
        /// RestResponse 返回 Record 类型，
        /// 命名空间 KaiheilaBot.Core.Models.Response 下
        /// </typeparam>
        /// <returns>反序列化得到的 Record 类型 T</returns>
        public static BaseResponse<T> Deserialize<T>(string jsonString) where T : BaseResponseData
        {
            return JsonSerializer.Deserialize<BaseResponse<T>>(jsonString);
        }
    }
}
