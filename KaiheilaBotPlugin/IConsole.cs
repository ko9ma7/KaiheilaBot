using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

//Todo: 把所有返回的数据打包成class而不是目前的JObject
namespace KaiheilaBot.Interface
{
    /// <summary>
    /// 操作处理的主要框架
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// 发送频道文字消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<JObject> SendGroupMessage(ChannelMessage message);
        /// <summary>
        /// 创建频道
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<JObject> CreateChannel(NewChannelMessage message);
        /// <summary>
        /// 删除频道
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<JObject> RemoveChannel(RemoveChannelMessage message);
        /// <summary>
        /// 获取机器人已加入频道
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<JObject> GetChannels();
        /// <summary>
        /// 撤回消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<JObject> RemoveMessage(RemoveMessageMessage message);
        /// <summary>
        /// 更换昵称
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<JObject> ChangeNick(ChangeNickMessage message);
        /// <summary>
        /// 获取服务器内的用户
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<JObject> GetServerMembers(GetServerMemberMessage message);
    }
}
