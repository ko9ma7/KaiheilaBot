using System.Threading.Tasks;
using RestSharp;

namespace KaiheilaBot.Core.Services.IServices
{
    public interface IHttpApiRequestService
    {
        public IHttpApiRequestService AddParameter<T>(string paramName, T paramValue);

        public IHttpApiRequestService SetMethod(Method method);

        public IHttpApiRequestService SetResourcePath(string path);

        public Task<IRestResponse> GetResponse();
    }
}
