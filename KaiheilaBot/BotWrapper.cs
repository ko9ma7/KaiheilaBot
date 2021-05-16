using KaiheilaBot.Core;
using KaiheilaBot.Interface;
using KaiheilaBot.Models;
using KaiheilaBot.Service;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace KaiheilaBot
{
    /// <summary>
    /// 最基础的Bot，创建后自动加载插件
    /// </summary>
    public class BotWrapper
    {
        private readonly IList<string> tempDlls = new List<string>();
        private readonly Container Container;
        private ILogService log;
        private IList<IPlugin> plugins = new List<IPlugin>();
        private AssemblyLoadContext context = new AssemblyLoadContext("plugin", true);
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
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = "Plugin";
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.LastWrite | NotifyFilters.Size;
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
            RemoveCache();
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
            foreach(var plugin in plugins)
            {
                if (completed)
                {
                    return;
                }
                switch(plugin.HandleType)
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
                        if(message.Data.Extra is Invited)
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
        /// <summary>
        /// 检测到Plugin文件夹的改动后自动重载所有插件用
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            context.Unload();
            context = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            RemoveCache();
            context = new AssemblyLoadContext("plugin", true);
            Register();
        }
        /// <summary>
        /// 注册插件
        /// </summary>
        private async void Register()
        {
            var type = typeof(IPlugin);
            var hub = Container.GetInstance<Shared>();
            hub.messageHub.ClearSubscriptions();
            foreach (var file in Directory.GetFiles("Plugin", "*.dll", SearchOption.AllDirectories))
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    if (!Directory.Exists("Temp"))
                    {
                        Directory.CreateDirectory("Temp");
                    }
                    var temp = Path.Combine(Environment.CurrentDirectory, "Temp", Path.GetRandomFileName());
                    tempDlls.Add(temp);
                    File.Copy(file, temp, true);
                    try
                    {
                        var assembly = context.LoadFromAssemblyPath(temp);
                        foreach (var i in assembly.GetTypes().Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract))
                        {
                            var plugin = (IPlugin)Activator.CreateInstance(i);
                            await plugin.PluginLoad(Container);
                            plugins.Add(plugin);
                            log.Debug(plugin.GetType().Name + " 插件已注册！");
                        }
                    }
                    catch(Exception ex)
                    {
                        //dll 加载错误，无视
                        log.Error(file + " 插件加载失败！" + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        var assembly = Assembly.LoadFrom(file);
                        foreach (var i in assembly.GetTypes().Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract))
                        {
                            var plugin = (IPlugin)Activator.CreateInstance(i);
                            plugins.Add(plugin);
                            log.Debug(plugin.GetType().Name + " 插件已注册！");
                        }
                    }
                    catch
                    {

                    }

                }
            }
            hub.messageHub.Subscribe<ReceiveMessage>(async message => {
                await Handle(message, plugins);
            });
        }
        /// <summary>
        /// 删除掉缓存文件
        /// </summary>
        private async void RemoveCache()
        {
            log.Information("正在卸载插件...");
            foreach (var plugin in plugins)
            {
                await plugin.PluginUnload(Container);
            }
            plugins.Clear();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var temp in tempDlls)
                    {
                        sb.AppendLine("del /f " + temp);
                    }
                    sb.AppendLine("(goto) 2>nul & del \"%~f0\"");
                    File.WriteAllText("remove.bat", sb.ToString());
                    Process.Start("remove.bat");
                }
                catch{

                }
            }
        }
    }
}
