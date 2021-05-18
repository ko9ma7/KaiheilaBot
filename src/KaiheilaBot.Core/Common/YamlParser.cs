using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace KaiheilaBot.Core.Common
{
    public static class YamlParser
    {
        public static async Task<T> Parse<T>(string yamlFilePath) where T : new()
        {
            var deserializer = new DeserializerBuilder().Build();

            var yamlReader = new StreamReader(yamlFilePath);
            var yamlString = await yamlReader.ReadToEndAsync();
            yamlReader.Close();

            return deserializer.Deserialize<T>(yamlString);
        }
    }
}
