using System.Threading.Tasks;
using KaiheilaBot.Core;
using KaiheilaBot.Interface;

namespace KaiheilaBot
{
    public class BotService:IBotService
    {
        private readonly ILogService logger;
        private readonly IBotWebSocket botWebsocket;
        /// <summary>
        /// 新建 Bot 实例
        /// </summary>
        /// <param name="token">Websocket 消息通知模式鉴权 Token</param>
        /// <param name="serilog">Serilog 实例，默认为 Null，将不输出与记录日志</param>
        public BotService(ILogService serilog, IBotWebSocket botWebsocket)
        {
            logger = serilog;
            logger.Information("实例化 Bot");
            this.botWebsocket = botWebsocket;
        }

        /// <summary>
        /// 开始运行机器人，此方法需要等待
        /// </summary>
        /// <param name="autoReconnect">是否在超时后自动重连，默认为 true</param>
        public async Task StartApp(bool autoReconnect = true)
        {
            logger.Information("正在启动...");
            logger.Information("自动重连：" + (autoReconnect == true ? "开启" : "关闭"));
            var status = 2;
            if (autoReconnect == true)
            {
                while (status != 0)
                {
                    status = await botWebsocket.Connect();
                    if (status == 1)
                    {
                        logger.Error("连接超时，已开启自动重连，将在 10 秒后重新开启连接");
                    }
                    else
                    {
                        logger.Warning("Websocket 连接关闭...已开启自动重连，将在 10 秒后重新开启连接");
                    }
                    await Task.Delay(10000);
                }
            }
            else
            {
                status = await botWebsocket.Connect();
                if (status == 0)
                {
                    logger.Information("Websocket 连接关闭...正常退出");
                }
                else
                {
                    logger.Warning("连接超时，未开启自动重连");
                }
            }
            logger.Information("已退出");
        }
    }
}