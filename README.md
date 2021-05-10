[![GitHub license](https://img.shields.io/github/license/LiamSho/KaiheilaBot?style=flat-square)](https://github.com/LiamSho/KaiheilaBot/blob/main/LICENSE)
![Nuget](https://img.shields.io/nuget/v/KaiheilaBot?style=flat-square)

# 开黑啦机器人开发框架

⚠️ 目前仍在开发中，基本可用，不保证稳定性和性能

## 使用
1. 从 [Nuget](https://www.nuget.org/packages/KaiheilaBot/) 安装

2. 从开黑啦开发者中心获取机器人 Websocket 模式下的 Token

```c#
const token = "Your Token Here";
```

3. 实例化 Serilog

```c#
var logger = new LoggerConfiguration()
		.MinimumLevel.Debug()
		.WriteTo.Console()
		.CreateLogger();
```

4. 实例化 Bot

```c#
var khlBot = new Bot(token, logger);
```

如果未传入 Serilog 实例 `logger` ，将不会记录和输出日志（***不推荐*** ）

在实例化之后，你可以调用 `KaiheilaBot.Globals` 类中的两个静态成员

* `Easy.MessageHub.MessageHub MessageHub` 简单的 Pub-Sub 模式消息队列，所有 Event 信令将会发送至此，请参考步骤4
* `RestSharp.RestClient RestClient` 一个用于发送 Http 请求的客户端，已经使用 token 添加了默认的身份验证 Header，具体使用请参考 [Getting Started | RestSharp](https://restsharp.dev/getting-started/getting-started.html#basic-usage)

5. 订阅 MessageHub 处理 Event

```c#
var id = Globals.MessageHub.Subscribe<JsonElement>(je =>
{
    logger.Debug(je.ToString());
});
```

MessageHub 保存 `System.Text.Json.JsonElement` 类实例，实例为 Websocket 返回的 Event 信令 Json 数据的根元素，请参考 [JsonElement Struct (System.Text.Json) | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.jsonelement?view=net-5.0) 和 [开黑啦开发者文档]([Websocket (kaiheila.cn)](https://developer.kaiheila.cn/doc/websocket#信令[0] EVENT))

6. 运行 Bot

```c#
await khlBot.StartApp();
```

`KaiheilaBot.Bot.StartApp()` 方法可以接收一个 `autoReconnect` 参数，默认值为 `true`，Bot 将在连接超时发生时重新连接，可以传入 `false` 关闭自动重连，请注意，若收到 Reconnect 信令导致的重新连接不受此参数限制

## 许可证

本项目使用 [MIT](./LICENSE) 许可证授权