using KaiheilaBot.Interface;
using System;

namespace KaiheilaBot.Models
{
    /// <summary>
    /// 消息收到EventArgs
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// 禁止手动创建，除非你知道你在干啥
        /// </summary>
        /// <param name="data"></param>
        /// <param name="request"></param>
        public MessageEventArgs(ReceiveMessageData data, IConsole request)
        {
            Data = data;
            Console = request;
        }
        /// <summary>
        /// 收到的数据
        /// </summary>
        public ReceiveMessageData Data { get; private set; }
        /// <summary>
        /// 操作处理的主要框架
        /// </summary>
        public IConsole Console { get; private set; }
        /// <summary>
        /// 发送者
        /// </summary>
        public string Sender
        {
            get
            {
                if(Data.AuthorId == "!")
                {
                    return null;
                }
                return Data.AuthorId;
            }
        }

        public string Taget
        {
            get
            {
                return Data.TargetId;
            }
        }
    }
}
