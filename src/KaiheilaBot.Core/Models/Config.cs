using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace KaiheilaBot.Models
{
    public class Config
    {
        private JToken dataConfig;
        /// <summary>
        /// 用于框架使用config
        /// </summary>
        public Config()
        {
            if (!File.Exists("config.json"))
            {
                File.WriteAllText("config.json", JsonConvert.SerializeObject(new Config(true), Formatting.Indented));
                throw new FileNotFoundException("不存在config.json，将会自动创建！请在设置好config.json后再打开！");
            }
            else
            {
                dataConfig = JsonConvert.DeserializeObject<JToken>(File.ReadAllText("config.json"));
            }
        }
        /// <summary>
        /// 用于内部创建config.json
        /// </summary>
        /// <param name="newConfig"></param>
        private Config(bool newConfig = true)
        {

        }

        public string Token
        {
            get
            {
                return dataConfig?.Value<string>("Token");
            }
        }
    }
}
