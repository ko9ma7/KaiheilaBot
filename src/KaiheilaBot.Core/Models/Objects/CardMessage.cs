using System.Collections.Generic;
using KaiheilaBot.Core.Models.Objects.CardMessages.Cards;

namespace KaiheilaBot.Core.Models.Objects
{
    public record CardMessage
    {
        public IEnumerable<Card> Cards { get; set; }
    }
}