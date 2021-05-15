using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace KaiheilaBot.Models
{
    public class Config
    {
        /// <summary>
        /// 用于框架使用config
        /// </summary>
        public Config()
        {
            if (!File.Exists("config.json"))
            {
                File.WriteAllText("config.json", JsonSerializer.Serialize(new Config(true)));
                throw new FileNotFoundException("不存在config.json，将会自动创建！请在设置好config.json后再打开！");
            }
        }
        /// <summary>
        /// 用于内部创建config.json
        /// </summary>
        /// <param name="newConfig"></param>
        private Config(bool newConfig = true)
        {

        }

        public string Token { get; set; }
    }
}
