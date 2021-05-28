using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using KaiheilaBot.Core.Common;
using KaiheilaBot.Core.Common.Serializers;
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
            var config = await YamlSerializer.Deserialize<Dictionary<string, string>>(configFilePath);
            if (config["Token"] == "null")
            {
                await Console.Error.WriteLineAsync("错误的配置：Token");
                return 1;
            }

            var rollingIntervalSetting = config["LoggerFileRollingInterval"] switch
            {
                "Month" => RollingInterval.Month,
                "Day" => RollingInterval.Day,
                "Hour" => RollingInterval.Hour,
                "Minute" => RollingInterval.Minute,
                _ => RollingInterval.Infinite
            };

            if (rollingIntervalSetting == RollingInterval.Infinite)
            {
                await Console.Error.WriteLineAsync("错误的配置：LoggerFileRollingInterval");
                return 1;
            }

            var loggerConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: config["LoggerTemplate"])
                .WriteTo.File(path: $"{config["LoggerFileDirectory"]}/log-.log", 
                    rollingInterval: rollingIntervalSetting,
                    outputTemplate: config["LoggerTemplate"],
                    shared: true);

            switch (config["LoggerMinimumLevel"])
            {
                case "Debug":
                    loggerConfiguration.MinimumLevel.Debug();
                    break;
                case "Information":
                    loggerConfiguration.MinimumLevel.Information();
                    break;
                default:
                    return 1;
            }

            Log.Logger = loggerConfiguration.CreateLogger();

            try
            {
                Log.Logger.Information("HOST - 正在启动 Generic Host...");
                await Run(instanceDirectory);
            }
            catch (Exception ex)
            {
                Log.Fatal($"HOST - Generic Host 因未知错误退出，错误：{ex}");
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
                        .AddSingleton<IHttpServerService, HttpServerService>()
                        .AddTransient<IHttpApiRequestService, HttpApiRequestService>();
                })
                .UseSerilog()
                .RunConsoleAsync();

    }
}