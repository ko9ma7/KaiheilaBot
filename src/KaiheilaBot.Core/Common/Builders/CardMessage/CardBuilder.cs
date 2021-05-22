using System.Collections.Generic;
using KaiheilaBot.Core.Models.Objects.CardMessages.Cards;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Common.Builders.CardMessage
{
    public class CardBuilder
    {
        private readonly Themes _theme;
        private readonly string _color;
        private readonly Sizes _size;

        private List<object> _modules = new();

        public CardBuilder(Themes theme, string color, Sizes size)
        {
            _theme = theme;
            _color = color;
            _size = size;
        }

        public CardBuilder AddModules(List<object> module)
        {
            _modules = module;
            return this;
        }

        public Card Build()
        {
            return new Card()
            {
                Theme = _theme,
                Color = _color,
                Size = _size,
                Modules = _modules
            };
        }
    }
}
