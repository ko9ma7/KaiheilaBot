
using KaiheilaBot.Interface;

namespace KaiheilaBot.Models
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessageEventArgs : MessageEventArgs
    {
        /// <summary>
        /// 禁止自己创建，除非你知道自己在干啥
        /// </summary>
        /// <param name="data"></param>
        /// <param name="request"></param>
        public TextMessageEventArgs(ReceiveMessageData data, IConsole request) : base(data, request)
        {
        }
        /// <summary>
        /// 文本内容
        /// </summary>
        public string Content
        {
            get
            {
                return Data.Content;
            }
        }
    }
}
