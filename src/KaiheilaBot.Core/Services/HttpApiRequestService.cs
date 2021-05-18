using System.Collections.Generic;
using System.Threading.Tasks;
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

        public IHttpApiRequestService AddParameter<T>(string paramName, T paramValue)
        {
            _params.Add(paramName, paramValue.ToString());
            return this;
        }

        public IHttpApiRequestService SetMethod(Method method)
        {
            _method = method;
            return this;
        }

        public IHttpApiRequestService SetResourcePath(string path)
        {
            _resourcePath = path;
            return this;
        }

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
