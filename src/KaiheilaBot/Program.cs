using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using KaiheilaBot.Core.Common.Builders.CardMessage;
using KaiheilaBot.Core.Models.Objects.CardMessages.Elements;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;
using Image = KaiheilaBot.Core.Models.Objects.CardMessages.Elements.Image;

namespace KaiheilaBot
{
    public static class Program
    {/*
        public static async Task Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var instanceDirectory = Path.Join(currentDirectory, "Instance");
            var pluginDirectory = Path.Join(instanceDirectory, "Plugins");
            var configFilePath = Path.Join(instanceDirectory, "config.yml");
            if (Directory.Exists(instanceDirectory) is false)
            {
                Directory.CreateDirectory(instanceDirectory);
            }

            if (Directory.Exists(pluginDirectory) is false)
            {
                Directory.CreateDirectory(pluginDirectory);
            }

            if (File.Exists(configFilePath) is false)
            {
                File.Copy(Path.Join(currentDirectory,"defaultConfig.yml"),
                    configFilePath);
            }
            
            await Core.KaiheilaBot.Startup(instanceDirectory);
        }*/

        public static void Main()
        {
            var cardMessage = new CardMessageBuilder()
                .AddCard(new CardBuilder(Themes.Primary, "#333333", Sizes.Lg)
                    .AddModules(new ModuleBuilder()
                        .AddSection(SectionModes.Left, 
                            new PlainText("321"), 
                            new object())
                        .AddSection(SectionModes.Right, 
                            new Kmarkdown("***123***"), 
                            new object())
                        .AddHeader(new PlainText("Hello World Header"))
                        .AddDivider()
                        .AddCountdown(1621664919440,
                            1621668519440,
                            CountdownModes.Second)
                        .AddImageGroup(new List<Image>()
                        {
                            {new("https://www.ascii-code.com/i/ascii-code.jpg")}
                        })
                        .Build())
                    .Build())
                .Build();
            
            Console.WriteLine(cardMessage);
        }
    }
}
