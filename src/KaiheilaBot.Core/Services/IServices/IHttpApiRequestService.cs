using System.Threading.Tasks;
using KaiheilaBot.Core.Models.Requests;
using RestSharp;

namespace KaiheilaBot.Core.Services.IServices
{
    public interface IHttpApiRequestService
    {
        public IHttpApiRequestService AddParameter<T>(string paramName, T paramValue);

        public IHttpApiRequestService AddPostBody<T>(string bodyParamName, T bodyParamValue);

        public IHttpApiRequestService SetMethod(Method method);

        public IHttpApiRequestService SetResourcePath(string path);

        public IHttpApiRequestService SetFileUpload(string filePath);
        
        public Task<IRestResponse> GetResponse<T>(T requestRecord) where T : BaseRequest;

        public Task<IRestResponse> GetResponse();
    }
}
