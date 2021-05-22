using System;
using System.Text;
using System.Text.Json;

namespace KaiheilaBot.Core.Common.Extensions
{
    public class JsonMinusSignNamingPolicyExtension : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var result = new StringBuilder();
            var arr = name.ToCharArray();
            arr[0] = (char) (arr[0] + 32);
            foreach (var ch in arr)
            {
                if(ch is >= 'A' and <= 'Z')
                {
                    result.Append('-');
                    result.Append((char)(ch + 32));
                    continue;
                }

                result.Append(ch);
            }

            return result.ToString();
        }
    }
}