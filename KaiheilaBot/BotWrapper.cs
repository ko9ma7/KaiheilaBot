using KaiheilaBot.Core;
using KaiheilaBot.Interface;
using KaiheilaBot.Interface.Services;
using KaiheilaBot.Models;
using KaiheilaBot.Service;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KaiheilaBot
{
    /// <summary>
    /// 最基础的Bot，创建后自动加载插件
    /// </summary>
    public class BotWrapper
    {

        private readonly Container Container;
        private ILogService log;
        private IPluginLoader<IPlugin> pluginLoader;
        /// <summary>
        /// 创建Bot
        /// </summary>
        public BotWrapper()
        {
            //注册DI
            Container = new Container();
            Container.Register<ILogService, LogService>(Lifestyle.Singleton);
            Container.Register<IBotService, BotService>();
            Container.Register<IBotWebSocket, BotWebsocket>();
            Container.Register<IBotRequest, BotRequest>();
            Container.Register<Shared>(Lifestyle.Singleton);
            Container.Register<Config>(Lifestyle.Singleton);
            Container.Register<IConsole, ConsoleService>();
            Container.Register<IPluginLoader<IPlugin>, PluginService<IPlugin>>();
        }
        /// <summary>
        /// 启动机器人所有功能
        /// </summary>
        /// <returns></returns>
        public async virtual Task Start()
        {
            if (!Directory.Exists("Plugin"))
            {
                Directory.CreateDirectory("Plugin");
            }
            if(Directory.Exists("Temp"))
            {
                foreach(var temp in Directory.GetFiles("Temp", "*.*", SearchOption.AllDirectories))
                {
                    try
                    {
                        File.Delete(temp);
                    }
                    catch
                    {
                        //Unable to delete, maybe next time
                    }
                }
            }
            FileSystemWatcher watcher = new FileSystemWatcher("Plugin");
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.dll";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            log = Container.GetInstance<ILogService>();
            log.Information("正在加载插件...");
            if (!Directory.Exists("Plugin"))
            {
                Directory.CreateDirectory("Plugin");
            }
            Register();
            log.Information("插件加载完毕！...");
            var bot = Container.GetInstance<IBotService>();
            await bot.StartApp();
        }
        /// <summary>
        /// 关闭之前，清理掉为了Hot Reload而创建的缓存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            pluginLoader.Unload();
        }
        /// <summary>
        /// 注册插件
        /// </summary>
        private async void Register()
        {
            var hub = Container.GetInstance<Shared>();
            hub.messageHub.ClearSubscriptions();
            pluginLoader = Container.GetInstance<IPluginLoader<IPlugin>>();
            var plugins = await pluginLoader.Load();
            hub.messageHub.Subscribe<ReceiveMessage>(async message =>
            {
                await Handle(message, plugins);
            });
        }
        /// <summary>
        /// 检测到Plugin文件夹的改动后自动重载所有插件用
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            pluginLoader.Unload();
            Register();
        }
        /// <summary>
        /// 综合分类给各插件的Handle功能
        /// </summary>
        /// <param name="message"></param>
        /// <param name="plugins"></param>
        /// <returns></returns>
        private async Task Handle(ReceiveMessage message, IList<IPlugin> plugins)
        {
            var completed = false;
            foreach (var plugin in plugins)
            {
                if (completed)
                {
                    return;
                }
                switch (plugin.HandleType)
                {
                    case EventType.ChannelTextMessage:
                        if (message.Data.Extra is ExtraText)
                        {
                            var author = (message.Data.Extra as ExtraText);
                            if (!author.Author.IsBot && !author.Author.IsSystem)
                            {
                                completed = await plugin.Handle(new TextMessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                            }
                        }
                        break;
                    case EventType.BotInvited:
                        if (message.Data.Extra is Invited)
                        {
                            completed = await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.ChannelCreated:
                        if (message.Data.Extra is NewChannel)
                        {
                            completed = await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.ChannelEdited:
                        if (message.Data.Extra is EditChannel)
                        {
                            completed = await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.ChannelRemoved:
                        if (message.Data.Extra is DeleteChannel)
                        {
                            completed = await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MemberChangeNick:
                        if (message.Data.Extra is MemberChangeNick)
                        {
                            completed = await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MemberJoinChannel:
                        if (message.Data.Extra is MemberJoin)
                        {
                            completed = await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MemberLeaveChannel:
                        if (message.Data.Extra is MemberLeave)
                        {
                            completed = await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MessageChanged:
                        if (message.Data.Extra is UpdateMessage)
                        {
                            completed = await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MessagePinned:
                        if (message.Data.Extra is PinnedMessage)
                        {
                            completed = await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MessageReacted:
                        if (message.Data.Extra is Reaction)
                        {
                            completed = await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MessageRemoved:
                        if (message.Data.Extra is DeleteMessage)
                        {
                            completed = await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                }
            }

        }
    }
}
