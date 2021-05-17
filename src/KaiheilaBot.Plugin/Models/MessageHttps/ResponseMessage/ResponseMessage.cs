

using Newtonsoft.Json.Linq;

namespace KaiheilaBot
{
    public class HttpResponseMessage<T>: HttpResponseMessage where T : BaseData
    {
        public T data { get; set; }
    }
    public class HttpResponseMessage
    {
        public string code { get; set; }
        public string message { get; set; }
        public override string ToString()
        {
            return JObject.FromObject(this).ToString();
        }
    }
}
