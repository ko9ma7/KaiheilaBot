using System.Collections.Generic;
using System.Threading.Tasks;
using KaiheilaBot.Core.Attributes;
using KaiheilaBot.Core.Common;
using KaiheilaBot.Core.Models.Requests;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace KaiheilaBot.Core.Services
{
    public class HttpApiRequestService : IHttpApiRequestService
    {
        private readonly ILogger<HttpApiRequestService> _logger;
        private readonly IConfiguration _configuration;

        private readonly Dictionary<string, string> _params = new();
        
        private Method _method = Method.GET;

        private string _resourcePath = "";
        
        public HttpApiRequestService(
            ILogger<HttpApiRequestService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// 添加 RestClient 请求参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="paramValue">参数内容</param>
        /// <typeparam name="T">参数类型，需要能够使用 ToString() 返回请求值</typeparam>
        /// <returns></returns>
        public IHttpApiRequestService AddParameter<T>(string paramName, T paramValue)
        {
            _params.Add(paramName, paramValue.ToString());
            _logger.LogDebug($"添加 RestRequest 参数：{paramName} - {paramValue.ToString()}");
            return this;
        }

        /// <summary>
        /// 设置 RestClient 请求方法
        /// </summary>
        /// <param name="method">请求方法 RestSharp.Method.XXX</param>
        /// <returns></returns>
        public IHttpApiRequestService SetMethod(Method method)
        {
            _logger.LogDebug($"设置 RestRequest 为 {method.ToString()}");
            _method = method;
            return this;
        }

        /// <summary>
        /// 设置请求资源地址，此地址在配置文件中的基地址的基础上添加
        /// </summary>
        /// <param name="path">资源地址</param>
        /// <returns></returns>
        public IHttpApiRequestService SetResourcePath(string path)
        {
            _resourcePath = path;
            return this;
        }

        /// <summary>
        /// 通过 RequestRecord 配置获取 Response
        /// </summary>
        /// <param name="requestRecord">RequestRecord 实例</param>
        /// <typeparam name="T">RequestRecord 类型，在命名空间 KaiheilaBot.Core.Models.Requests 下</typeparam>
        /// <returns>RestResponse</returns>
        public async Task<IRestResponse> GetResponse<T>(T requestRecord) where T : BaseRequest
        {
            if (requestRecord == null)
            {
                return null;
            }
            foreach (var propertyInfo in requestRecord.GetType().GetProperties())
            {
                var val = propertyInfo.GetValue(requestRecord);
                switch (propertyInfo.Name)
                {
                    case "RequestMethod":
                        if (val is Method method)
                        {
                            SetMethod(method);
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    case "ResourcePath":
                        if (val is string path)
                        {
                            SetResourcePath(path);
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    case "UploadingFiles":
                        // TODO: 文件操作，暂不支持，完成 API 建模后再做
                        break;
                    default:
                        if (val is not null)
                        {
                            var paramValue = val.ToString();
                            
                            // public static T GetAttribute<T>(this MemberInfo prop) => From RestSharp.Extensions
                            var paramNameAttribute = propertyInfo.GetAttribute<ParameterNameAttribute>();
                            var paramName = paramNameAttribute.GetName();
                            
                            AddParameter(paramName, paramValue);
                        }
                        break;
                }
            }
            
            return await GetResponse();
        }

        /// <summary>
        /// 通过手动设置 HttpApiRequestService 获取 Response
        /// </summary>
        /// <returns>RestResponse</returns>
        public async Task<IRestResponse> GetResponse()
        {
            var client = new RestClient(_configuration["HttpApiBaseUrl"]);
            var request = new RestRequest(_resourcePath, _method);
            request.AddHeader("Authorization", $"Bot {_configuration["Token"]}");
            foreach (var (paramName, paramValue) in _params)
            {
                request.AddParameter(paramName, paramValue);
            }
            return await client.ExecuteAsync(request);
        }
    }
}
