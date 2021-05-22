using System.Collections.Generic;
using System.Text.Json.Serialization;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;

namespace KaiheilaBot.Core.Models.Objects.CardMessages.Structures
{
    public record Paragraph : ISectionText
    {
        [JsonPropertyName("type")]
        public CardMessageTypes Type { get; init; } = CardMessageTypes.Paragraph;
        
        [JsonPropertyName("cols")]
        public int Columns { get; set; }

        /// <summary>
        /// object 类型便于序列化，只接受 Context, Kmarkdown, PlainText => IParagraphField
        /// </summary>
        [JsonPropertyName("fields")]
        public IEnumerable<object> Fields { get; set; }

        public Paragraph(int columns, IEnumerable<IParagraphField> fields)
        {
            Columns = columns;
            Fields = fields;
        }
    }
}
