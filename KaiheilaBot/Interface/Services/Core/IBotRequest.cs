using RestSharp;
using System.Threading.Tasks;

namespace KaiheilaBot.Interface
{
    public interface IBotRequest
    {
        IBotRequest AddParameter<T>(string parameterName, T parameter);
        IBotRequest SetMethod(Method method);
        IBotRequest SetUrl(string url);
        IBotRequest AddFile(string filePath);
        Task<RestResponse> GetResponse();
    }
}
