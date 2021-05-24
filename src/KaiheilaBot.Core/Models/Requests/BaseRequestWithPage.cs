namespace KaiheilaBot.Core.Models.Requests
{
    public abstract record BaseRequestWithPage : BaseRequest
    {
        public abstract int Page { get; set; }

        public abstract int PageSize { get; set; }

        public abstract string Sort { get; set; }
    }
}
