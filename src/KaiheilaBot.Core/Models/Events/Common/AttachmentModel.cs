namespace KaiheilaBot.Core.Models.Events.Common
{
    public record AttachmentModel
    {
        public string Type { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string FileType { get; set; }

        public long Size { get; set; }

        public long Duration { get; set; }

        public long Width { get; set; }

        public long Height { get; set; }
    }
}