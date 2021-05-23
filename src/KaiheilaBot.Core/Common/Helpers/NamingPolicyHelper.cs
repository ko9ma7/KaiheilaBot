using System.Text;

namespace KaiheilaBot.Core.Common.Helpers
{
    public static class NamingPolicyHelper
    {
        public static string UnderlineToCapital(string str)
        {
            var result = new StringBuilder();
            var arr = str.ToCharArray();
            result.Append((char) (arr[0] - 32));
            for (var i = 1; i < arr.Length; i++)
            {
                if (arr[i] == '_')
                {
                    result.Append((char) (arr[i + 1] - 32));
                    i++;
                    continue;
                }
                result.Append(arr[i]);
            }

            return result.ToString();
        }
    }
}