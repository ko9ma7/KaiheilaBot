using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using KaiheilaBot.Core.Common;
using KaiheilaBot.Core.Services;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace KaiheilaBot.Core
{
    public static class KaiheilaBot
    {
        /// <summary>
        /// 启动 Host
        /// </summary>
        /// <param name="instanceDirectory">Instance 文件夹</param>
        public static async Task<int> Startup(string instanceDirectory)
        {
            var configFilePath = Path.Join(instanceDirectory, "config.yml");
            var config = await YamlParser.Parse<Dictionary<string, string>>(configFilePath);
            Console.WriteLine(config["Token"]);
            if (config["Token"] == "null")
            {
                return 1;
            }

            // TODO: 通过配置文件设置Logger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithAssemblyName()
                .Enrich.WithAssemblyVersion()
                .WriteTo.Console()
                .WriteTo.File(path: @"Logs/log-.log", rollingInterval: RollingInterval.Day, shared: true)
                .CreateLogger();

            try
            {
                Log.Logger.Information("Starting host...");
                await Run(instanceDirectory);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

            return 0;
        }

        /// <summary>
        /// 构建与运行 Generic Host
        /// </summary>
        private static async Task Run(string instanceDirectory) =>
            await new HostBuilder()
                .ConfigureAppConfiguration((_, builder) =>
                {
                    builder
                        .AddYamlFile(Path.Join(instanceDirectory, "config.yml"), optional: false)
                        .AddInMemoryCollection(new Dictionary<string, string>
                        {
                            {"PluginFolder", instanceDirectory}
                        });
                })
                .ConfigureServices((_, services) =>
                {
                    services
                        .AddHostedService<BotHostedService>()
                        .AddSingleton<IBotWebsocketService, BotWebsocketService>()
                        .AddSingleton<IMessageHubService, MessageHubService>()
                        .AddSingleton<IPluginService, PluginService>()
                        .AddTransient<IHttpApiRequestService, HttpApiRequestService>();
                })
                .UseSerilog()
                .RunConsoleAsync();

    }
}