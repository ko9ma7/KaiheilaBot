using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using KaiheilaBot.Core.Common.Extensions;
using KaiheilaBot.Core.Models.Objects.CardMessages.Cards;

namespace KaiheilaBot.Core.Common.Builders.CardMessage
{
    public class CardMessageBuilder
    {
        private readonly List<Card> _cards = new();
        
        public CardMessageBuilder AddCard(Card card)
        {
            _cards.Add(card);
            return this;
        }

        public string Build()
        {
            var options = new JsonSerializerOptions()
            {
                Converters = {new JsonStringEnumConverter(new JsonMinusSignNamingPolicyExtension())}
            };

            return JsonSerializer.Serialize(_cards, options);
        }
    }
}
