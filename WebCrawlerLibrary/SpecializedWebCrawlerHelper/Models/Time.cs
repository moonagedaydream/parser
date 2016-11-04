namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper.Models {
    public class Time {
        public int Id { get; set; }
        public long Milliseconds { get; set; }

        public virtual Domain Domain { get; set; }
    }
}
