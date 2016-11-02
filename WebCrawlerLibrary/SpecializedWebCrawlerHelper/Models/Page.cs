using System.Collections.Generic;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper.Models {
    public class Page {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Page() {
            Urls = new HashSet<Url>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public int Size { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Url> Urls { get; set; }
    }
}
