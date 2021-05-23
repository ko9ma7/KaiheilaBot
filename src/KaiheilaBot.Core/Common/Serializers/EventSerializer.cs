using System;
using System.Text.Json;
using KaiheilaBot.Core.Models.Events;

namespace KaiheilaBot.Core.Common.Serializers
{
    public static class EventSerializer
    {
        /// <summary>
        /// 反序列化 Event 信令 0, Type 不为 Number 类型的信息字符串
        /// </summary>
        /// <param name="jsonString">Json 字符串</param>
        /// <typeparam name="T">
        /// Event 信令 0 获取的 Record 类型，
        /// 命名空间 KaiheilaBot.Core.Models.Events.xxxEvents 下，
        /// xxx 不为 MessageRelate
        /// </typeparam>
        /// <returns>反序列化得到的 Record 类型 T</returns>
        public static BaseEvent<T> DeserializeEvent<T>(string jsonString) where T : IBaseEventExtraBody
        {
            try
            {
                return JsonSerializer.Deserialize<BaseEvent<T>>(jsonString);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 反序列化 Event 信令 0, Type 为 Number 类型的信息字符串
        /// </summary>
        /// <param name="jsonString">Json 字符串</param>
        /// <typeparam name="T">
        /// Event 信令 0 获取的 Record 类型，
        /// 命名空间 KaiheilaBot.Core.Models.Events.MessageRelatedEvents 下，
        /// </typeparam>
        /// <returns>反序列化得到的 Record 类型 T</returns>
        public static BaseMessageEvent<T> DeserializeMessageEvent<T>(string jsonString) where T : IBaseMessageEventDataExtra
        {
            try
            {
                return JsonSerializer.Deserialize<BaseMessageEvent<T>>(jsonString);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
