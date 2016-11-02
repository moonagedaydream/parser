using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper.Models {
    public partial class CrawlerContext : DbContext {
        public CrawlerContext()
            : base("name=CrawlerConnection") {
        }

        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Domain> Domains { get; set; }
        public virtual DbSet<Subdomain> Subdomains { get; set; }
        public virtual DbSet<Url> Urls { get; set; }
    }
}
