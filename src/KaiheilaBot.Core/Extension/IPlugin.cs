using System.Text.Json;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension
{
    /// <summary>
    /// 扩展插件公共接口，所有插件需要实现此接口
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 插件初始化，将在 MessageHub 订阅之前执行，此时已完成 Host 构建，
        /// 此时将会传入 Logger 和 HttpApiRequestService，插件应保存这两个实例
        /// </summary>
        /// <param name="logger">ILogger 实例</param>
        /// <param name="httpApiRequestService">IHttpApiRequestService 实例</param>
        /// <returns></returns>
        public Task Initialize(ILogger<IPlugin> logger, 
            IHttpApiRequestService httpApiRequestService);

        /// <summary>
        /// 插件执行，此方法将订阅 MessageHub 事件
        /// </summary>
        /// <returns></returns>
        public Task Execute(JsonElement data);
        
        /// <summary>
        /// 插件卸载，将在收到卸载指令，程序退出前执行
        /// </summary>
        /// <returns></returns>
        public Task Unload();
    }
}