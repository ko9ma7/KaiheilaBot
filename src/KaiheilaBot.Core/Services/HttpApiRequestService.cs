using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaiheilaBot.Core.Attributes;
using KaiheilaBot.Core.Common.Extensions;
using KaiheilaBot.Core.Models.Requests;
using KaiheilaBot.Core.Models.Requests.Media;
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
        private readonly Dictionary<string, string> _body = new();
        
        private Method _method = Method.GET;
        private string _filePath = string.Empty;
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
            _logger.LogDebug($"HttpRequest - 添加 RestRequest 参数：{paramName} - {paramValue.ToString()}");
            return this;
        }

        /// <summary>
        /// 添加 Post 请求时的 Body 参数
        /// </summary>
        /// <param name="bodyParamName">参数名</param>
        /// <param name="bodyParamValue">参数内容</param>
        /// <typeparam name="T">参数类型，需要能够使用 ToString() 返回请求值</typeparam>
        /// <returns></returns>
        public IHttpApiRequestService AddPostBody<T>(string bodyParamName, T bodyParamValue)
        {
            _body.Add(bodyParamName, bodyParamValue.ToString());
            _logger.LogDebug($"HttpRequest - 添加 RestRequest Post Body 参数：{bodyParamName} - {bodyParamValue.ToString()}");
            return this;
        }

        /// <summary>
        /// 设置 RestClient 请求方法
        /// </summary>
        /// <param name="method">请求方法 RestSharp.Method.XXX</param>
        /// <returns></returns>
        public IHttpApiRequestService SetMethod(Method method)
        {
            _method = method;
            _logger.LogDebug($"HttpRequest - 设置 RestRequest 为 {_method.ToString()}");

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
            _logger.LogDebug($"HttpRequest - 设置 ResourcePath 为 {_resourcePath}");
            return this;
        }

        /// <summary>
        /// 设置为调用上传文件接口，之后请直接调用 GetResponse()
        /// </summary>
        /// <returns></returns>
        public IHttpApiRequestService SetFileUpload(string filePath)
        {
            _method = Method.POST;
            _resourcePath = "asset/create";
            _filePath = filePath;
            _logger.LogDebug($"HttpRequest - 文件上传模式，文件路径：{_filePath}");
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

            _logger.LogDebug($"HttpRequest - 从 RequestRecord 配置获取 Response，配置类型：{typeof(T)}");
            
            if (typeof(T) == typeof(CreateAssetRequest))
            {
                var filePath = (string) requestRecord.GetType().GetProperties()
                    .First(x => x.Name == "FilePath").GetValue(requestRecord);
                SetFileUpload(filePath);
                
                return await GetResponse();
            }
            
            var paramDictionary = new Dictionary<string, string>();
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
                    case "Page":
                        if (val is int page)
                        {
                            AddParameter("page", page);
                        }
                        break;
                    case "PageSize":
                        if (val is int pageSize)
                        {
                            AddParameter("page_size", pageSize);
                        }
                        break;
                    case "Sort":
                        if (val is string sort)
                        {
                            AddParameter("sort", sort);
                        }
                        break;
                    default:
                        if (val is not null)
                        {
                            var paramValue = val.ToString();
                            
                            var paramNameAttribute = propertyInfo.GetAttribute<ParameterNameAttribute>();
                            var paramName = paramNameAttribute.GetName();
                            
                            paramDictionary.Add(paramName, paramValue);
                        }
                        break;
                }
            }

            if (_method == Method.GET)
            {
                foreach (var (key,value) in paramDictionary)
                {
                    AddParameter(key, value);
                }
            }
            else
            {
                foreach (var (key, value) in paramDictionary)
                {
                    AddPostBody(key, value);
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
            if (_filePath != string.Empty)
            {
                request.AddFile("file", _filePath,"form-data");
            }
            else
            {
                foreach (var (paramName, paramValue) in _params)
                {
                    request.AddParameter(paramName, paramValue);
                }
                if (_method == Method.POST)
                {
                    request.AddJsonBody(_body);
                }
            }
            
            return await client.ExecuteAsync(request);
        }
    }
}
