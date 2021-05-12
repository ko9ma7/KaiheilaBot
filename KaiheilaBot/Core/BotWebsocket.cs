using System;
using System.Net;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using RestSharp;
using Websocket.Client;
using Timer = System.Timers.Timer;

namespace KaiheilaBot.Core
{
    internal class BotWebsocket
    {
        private readonly Timer _pingTimer;
        private readonly Timer _pingTimoutTimer;
        private BotStatus _status;
        private WebsocketClient _client;
        private ManualResetEvent _event;
        private int _latestSn;
        private int _pingTimeoutResentTimes;
        
        /// <summary>
        /// 初始化 Bot Websocket.
        /// </summary>
        public BotWebsocket()
        {
            _status = BotStatus.Initialized;
            Log.Information("机器人状态：初始化");
            
            _pingTimer = new Timer() {Interval = 30000, Enabled = false, AutoReset = true};
            _pingTimer.Elapsed += SendingPing;
            Log.Information("已设置 Ping 定时器");

            _pingTimoutTimer = new Timer() {Interval = 6000, Enabled = false, AutoReset = false};
            _pingTimoutTimer.Elapsed += PingTimeout;
            Log.Information("已设置 Ping 超时检测定时器");

            _pingTimeoutResentTimes = 2;
            Log.Information($"已设置 Ping 超时重试次数：{_pingTimeoutResentTimes}");

            _latestSn = 0;
            Log.Information("已重设 Sn 编号");
        }

        /// <summary>
        /// 连接 Websocket，开始获取与预处理信息.
        /// </summary>
        /// <returns>0：正常退出；1：超时退出；</returns>
        public async Task<int> Connect()
        {
            var url = await GetWebsocketUrl();

            if (_status != BotStatus.Gateway)
            {
                Log.Fatal("获取 Websocket Url 失败，正在退出...");
                return 1;
            }

            _event = new ManualResetEvent(false);

            using (_client = new WebsocketClient(new Uri(url)))
            {
                Log.Information("已建立 Websocket Client");
                _client.ReconnectTimeout = null;
                
                _client.MessageReceived.Subscribe(MessageProcessor);
                Log.Information("已订阅 Message Received 事件");
                
                Log.Information("开始连接");

                var task = _client.Start();
                task.Wait();

                _event.WaitOne();
                
                Log.Information("连接已关闭");
            }

            return _status == BotStatus.Timeout ? 1 : 0;
        }
        
        /// <summary>
        /// 从 Gateway 获取 Websocket Url. 将会尝试获取 3 次，失败将会返回 Null.
        /// </summary>
        /// <returns>Websocket Url 或者 Null</returns>
        private async Task<string> GetWebsocketUrl()
        {
            Log.Debug("准备从 Gateway 获取 Websocket Url");
            for (var i = 1; i <= 3; i++)
            {
                Log.Information($"第 {i.ToString()} 次尝试获取 Websocket Url");
                var response = await new BotRequest()
                    .SetUrl("gateway/index")
                    .SetMethod(Method.GET)
                    .AddParameter<int>("compress", 0)
                    .GetResponse();
                
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (i != 3)
                    {
                        Log.Warning($"第 {i.ToString()} 次获取 Websocket Url 失败，将在 {2*i} 秒后重试");
                        await Task.Delay(2000 * i);
                    }
                    else
                    {
                        Log.Error($"第 {i.ToString()} 次获取 Websocket Url 失败");
                    }
                    continue;
                }
                
                Log.Information("从 Gateway 获取 Websocket Url 成功");
                var data = response.Content;
                var json = JsonDocument.Parse(data).RootElement;
                var url = json.GetProperty("data").GetProperty("url").GetString();
                
                _status = BotStatus.Gateway;
                Log.Information("机器人状态：已获取 Websocket Url");
                
                return url;
            }
            Log.Fatal("获取 Websocket Url 失败");
            return null;
        }
        
        /// <summary>
        /// Message 预处理
        /// </summary>
        /// <param name="message">从 Websocket 收到的 Response Message</param>
        private void MessageProcessor(ResponseMessage message)
        {
            switch (message.MessageType)
            {
                case WebSocketMessageType.Binary:
                    Log.Warning("收到不支持的 Binary 信息，放弃处理");
                    return;
                case WebSocketMessageType.Close:
                    Log.Warning("收到不支持的 Close 信息，放弃处理");
                    return;
                case WebSocketMessageType.Text:
                    Log.Information("收到 Text 信息");
                    break;
                default:
                    Log.Error("收到未知格式信息");
                    return;
            }
            
            var json = JsonDocument.Parse(message.Text).RootElement;
            var type = json.GetProperty("s").GetInt32();
            if (type == 1)
            {
                Log.Information("收到 Hello 信令，连接建立");
                _status = BotStatus.Established;
                Log.Information("机器人状态：连接建立");
                
                _pingTimer.Enabled = true;
                Log.Information("开启 Ping 定时器");
                SendingPing(null,null);
                return;
            }

            switch (type)
            {
                case 0:
                    var sn = json.GetProperty("sn").GetInt32();
                    Log.Information($"收到 Event 信令，Sn = {sn}");
                    
                    Log.Information($"发布 Event 信令 Sn = {sn} 数据至 Message Hub，类型：{json.GetType()}");
                    Globals.MessageHub.Publish(json);
                    
                    _latestSn = sn;
                    break;
                case 3:
                    _pingTimoutTimer.Enabled = false;
                    _pingTimeoutResentTimes = 2;
                    _status = BotStatus.Established;
                    if (_pingTimer.Enabled == false)
                    {
                        _pingTimer.Enabled = true;
                        Log.Warning("重新开启 Ping 定时器");
                    }
                    Log.Information("收到 Pong 信令");
                    break;
                case 4:
                    Log.Information("收到 Reconnect 信令");
                    Log.Information("即将断开连接");
                    _status = BotStatus.Reconnect;
                    Log.Information("机器人状态：重新连接");
                    _event.Set();
                    break;
                case 5:
                    Log.Information("收到 Resume 信令");
                    break;
            }
        }

        /// <summary>
        /// 发送 Ping 信令
        /// </summary>
        /// <param name="sender">Timer Sender</param>
        /// <param name="e">Timer Elapsed Event Args</param>
        private void SendingPing(object sender, ElapsedEventArgs e)
        {
            _client.Send($"{{\"s\":2,\"sn\":{_latestSn.ToString()}}}");
            _pingTimoutTimer.Enabled = true;
            Log.Information($"已发送 Ping 信令，Sn = {_latestSn}");
        }

        /// <summary>
        /// Ping 超时，未再时间内收到 Pong，停止 Task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PingTimeout(object sender, ElapsedEventArgs e)
        {
            Log.Error("未在时间内收到 Pong 信令"); 
            _status = BotStatus.Timeout; 
            Log.Warning("机器人状态：超时");
            
            _pingTimer.Enabled = false; 
            Log.Information("已关闭 Ping 定时器");

            if (_pingTimeoutResentTimes == 0)
            {
                Log.Fatal("连接超时，准备停止 Websocket Task"); 
                _event.Set();
            }
            else
            {
                _pingTimeoutResentTimes--;
                Log.Warning($"将在 {2 * (2 - _pingTimeoutResentTimes)} 秒后重新发送 Ping 信令");
                Thread.Sleep(2000 * (2 - _pingTimeoutResentTimes));
                SendingPing(null,null);
            }
        }

        /// <summary>
        /// Bot 状态标志
        /// </summary>
        private enum BotStatus 
        {
            /// <summary>
            /// Bot 初始化
            /// </summary>
            Initialized,
            /// <summary>
            /// Bot 已成功获取 Websocket Url
            /// </summary>
            Gateway,
            /// <summary>
            /// Bot 已成功连接至 Websocket
            /// </summary>
            Established,
            /// <summary>
            /// Bot 连接超时
            /// </summary>
            Timeout,
            /// <summary>
            /// Bot 收到 Reconnect 信令
            /// </summary>
            Reconnect
        }
    }
}