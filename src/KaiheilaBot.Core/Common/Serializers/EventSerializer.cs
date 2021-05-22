using System.Text.Json;
using KaiheilaBot.Core.Models.Events;

namespace KaiheilaBot.Core.Common.Serializers
{
    public static class EventSerializer
    {
        /// <summary>
        /// 反序列化 Event 信令 0 信息字符串
        /// </summary>
        /// <param name="jsonString">Json 字符串</param>
        /// <typeparam name="T">
        /// Event 信令 0 获取的 Record 类型，
        /// 命名空间 KaiheilaBot.Core.Models.Events.xxxEvents 下
        /// </typeparam>
        /// <returns>反序列化得到的 Record 类型 T</returns>
        public static BaseEvent<T> Deserialize<T>(string jsonString) where T : IBaseEventExtraBody
        {
            return JsonSerializer.Deserialize<BaseEvent<T>>(jsonString);
        }
    }
}
