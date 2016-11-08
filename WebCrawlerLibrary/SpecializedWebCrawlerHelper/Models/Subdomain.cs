namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper.Models {
    public class Subdomain {

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Domain Domain { get; set; }

    }
}
