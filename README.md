<img width="25%" src="khlsharp.svg" alt="logo">

<p style="text-align: center">
  <img src="https://img.shields.io/github/license/LiamSho/KaiheilaBot?style=flat-square">
  <img src="https://img.shields.io/nuget/v/KaiheilaBot.Core?style=flat-square">
  <img src="https://img.shields.io/github/last-commit/KHLSharp/KaiheilaBot/dev?label=%22dev%22%20brach%20commit&style=flat-square">
  <img src="https://img.shields.io/github/workflow/status/KHLSharp/KaiheilaBot/dotnet-build?style=flat-square">
</p>

# KaiheilaBot - .NET 开黑啦机器人框架

## :robot: 简介 :robot:

KaiheilaBot 是一个可以使用插件扩展的开黑啦机器人框架，你可以为机器人编写插件来实现一些功能，为开黑啦机器人的开发提供便利，以及一种模块化的功能开发方式。

## :books: 文档 :books:

[KaiheilaBot 开发文档](https://khlsharp.github.io/khlsharp-documents/)

## :electric_plug: 关于插件 :electric_plug:

框架的核心 `KaiheilaBot.Core` 提供了一系列的接口，可以通过 Nuget 安装 (KaiheilaBot.Core)[] 后来进行插件开发。

* 通过继承和实现接口，处理开黑啦 Websocket 发送的共 37 种事件，每一种事件对应一个接口
* 内置了一个简易 Http Api 服务器，可以通过配置文件选择是否开启，插件可以通过继承和实现对应接口来处理发送到这个 Http Api 服务器的消息
* 每个插件之间互相隔离，除了核心已有的公共依赖之外，每个插件可以拥有自己的对其他 Nuget 包的依赖关系，且每个插件的依赖不会互相影响
* 核心提供了向开黑啦服务器发送 HTTP 请求的方法，提供 `ILogger` 日志记录接口
* 可以使用 `CardMessageBuilder` 类来建立一个卡片消息

关于插件系统和插件开发，请查看 [KaiheilaBot 开发文档](https://khlsharp.github.io/khlsharp-documents/)

## :calendar: TODO :calendar:

请查看 [Projects · KHLSharp/KaiheilaBot)](https://github.com/KHLSharp/KaiheilaBot/projects)

## :book: 许可证 :book:

本项目使用 [MIT](./LICENSE) 许可证授权
