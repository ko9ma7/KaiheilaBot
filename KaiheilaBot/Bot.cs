using System.Threading.Tasks;
using Easy.MessageHub;
using KaiheilaBot.Core;
using RestSharp;
using Serilog.Core;

namespace KaiheilaBot
{
    public class Bot
    {
        /// <summary>
        /// 新建 Bot 实例
        /// </summary>
        /// <param name="token">Websocket 消息通知模式鉴权 Token</param>
        /// <param name="serilog">Serilog 实例，默认为 Null，将不输出与记录日志</param>
        public Bot(string token, Logger serilog = null)
        {
            Log.Logger = serilog;
            Log.Information("实例化 Bot");
            
            Globals.RestClient = new RestClient("https://www.kaiheila.cn/api/v3/");
            Globals.RestClient.AddDefaultHeader("Authorization", $"Bot {token}");
            Log.Information("实例化 REST Client");
            
            Globals.MessageHub = new MessageHub();
            Log.Information("实例化 Message Hub");

            Globals.LatestSn = 0;
            Log.Information("重设 Sn 编号");
        }

        /// <summary>
        /// 开始运行机器人，此方法需要等待
        /// </summary>
        /// <param name="autoReconnect">是否在超时后自动重连，默认为 true</param>
        public async Task StartApp(bool autoReconnect = true)
        {
            Log.Information("正在启动...");
            Log.Information("自动重连：" + (autoReconnect == true ? "开启" : "关闭"));
            var status = 2;
            if (autoReconnect == true)
            {
                while (status != 0)
                {
                    status = await new BotWebsocket().Connect();
                    if (status == 1)
                    {
                        Log.Error("连接超时，准备重连...");
                    }
                }
            }
            else
            {
                while (status == 2)
                {
                    status = await new BotWebsocket().Connect();
                }
            }
            Log.Information("已退出");
        }
    }
}