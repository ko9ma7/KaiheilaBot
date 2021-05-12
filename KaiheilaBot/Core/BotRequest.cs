using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace KaiheilaBot.Core
{
    public class BotRequest
    {
        private Dictionary<string, string> Parameters { get;}
        private Method Method { get; set; }
        private string Url { get; set; }
        private string FilePath { get; set; }
        
        /// <summary>
        /// 新建一个 HTTP 请求，已设置好请求鉴权 Header
        /// </summary>
        public BotRequest()
        {
            Parameters = new Dictionary<string, string>();
            Method = Method.GET;
            FilePath = null;
        }

        /// <summary>
        /// 添加一个参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="parameter">参数值</param>
        /// <typeparam name="T">参数类型</typeparam>
        /// <returns>当前 BotRequest 实例</returns>
        public BotRequest AddParameter<T>(string parameterName, T parameter)
        {
            var param = parameter.ToString();
            Parameters.Add(parameterName, param);
            return this;
        }
        
        /// <summary>
        /// 设置 Http 请求方法
        /// </summary>
        /// <param name="method">方法类型</param>
        /// <returns>当前 BotRequest 实例</returns>
        public BotRequest SetMethod(Method method)
        {
            Method = method;
            return this;
        }

        /// <summary>
        /// 设置请求资源地址，基地址 https://www.kaiheila.cn/api/v3/
        /// </summary>
        /// <param name="url">资源地址</param>
        /// <returns>当前 BotRequest 实例</returns>
        public BotRequest SetUrl(string url)
        {
            Url = url;
            return this;
        }

        /// <summary>
        /// 请求创建媒体资源接口，不能和其他配置共存，将会覆盖其他配置
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <example>
        /// var response = new BotRequest()
        ///     .AddFile("path/to/file.extension")
        ///     .GetResponse();
        /// </example>
        /// <returns>当前 BotRequest 实例</returns>
        public BotRequest AddFile(string filePath)
        {
            Url = "asset/create";
            FilePath = filePath;
            Method = Method.POST;
            return this;
        }
        
        /// <summary>
        /// 运行 Request 来获取 Response
        /// </summary>
        /// <returns>RestResponse 实例</returns>
        public async Task<RestResponse> GetResponse()
        {
            var request = new RestRequest(Url, Method);
            if (FilePath == null)
            {
                foreach (var (key,value) in Parameters)
                {
                    request.AddParameter(key, value);
                }
            }
            else
            {
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddFile("file", FilePath); 
            }

            var response = await Globals.RestClient.ExecuteAsync(request);
            return (RestResponse) response;
        }
    }
}