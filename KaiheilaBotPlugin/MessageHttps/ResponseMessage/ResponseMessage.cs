

using Newtonsoft.Json.Linq;

namespace KaiheilaBot
{
    public class HttpResponseMessage<T> where T : BaseData
    {
        public string code { get; set; }
        public string message { get; set; }
        public T data { get; set; }
        public override string ToString()
        {
            return JObject.FromObject(this).ToString();
        }
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
