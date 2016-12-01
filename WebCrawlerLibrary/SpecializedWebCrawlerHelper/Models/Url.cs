using System;
using System.Collections.Generic;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper.Models {


    public class Url {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Url() {
            Pages = new HashSet<Page>();
        }

        public int UrlId { get; set; }
        public string Text { get; set; }
        public string HashCode { get; set; }
        public bool Working { get; set; }
        public bool ExternalUrl { get; set; }
        public int? ErrorCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Page> Pages { get; set; }
        public virtual Domain Domain { get; set; }
        public virtual Domain BaseDomain { get; set; }
        public virtual Subdomain Subdomain { get; set; }
    }
}
