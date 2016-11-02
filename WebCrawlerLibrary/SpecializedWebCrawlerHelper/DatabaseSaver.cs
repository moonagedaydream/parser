using System;
using System.Data.Entity;
using System.Linq;
using ParserLibrary.HtmlParsers;
using WebCrawlerLibrary.SpecializedWebCrawlerHelper.Models;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper {
    class DatabaseSaver {

        private readonly string mainUrl;
        private readonly CrawlerContext context;
        private readonly HtmlParser parser;
        private Page page;

        private void AddPage() {
            page = new Page {
                Content = parser.Text,
                Size = Convert.ToInt32(parser.Size / 1024)
            };

            context.Pages.Add(page);
            context.SaveChanges();
        }

        private static string GetDomainName(Uri link) {
            string[] hostParts = link.Host.Split('.');
            return String.Join(".", hostParts.Skip(Math.Max(0, hostParts.Length - 2)).Take(2));
        }

        private Domain ProcessDomain(string domainName) {
            Domain domain = null;
            domain = context.Domains.FirstOrDefault(d => d.Name.Equals(domainName));
            if (domain == null) {
                domain = new Domain { Name = domainName };
                context.Domains.Add(domain);
                context.SaveChanges();
                }
            return domain;
        }

        private Subdomain ProcessSubdomain(string subdomainName) {
            Subdomain subdomain = null;
            if (!string.IsNullOrEmpty(subdomainName)) {
                subdomain = context.Subdomains.FirstOrDefault(s => s.Name.Equals(subdomainName));
                if (subdomain == null) {
                    subdomain = new Subdomain { Name = subdomainName };
                    context.Subdomains.Add(subdomain);
                    context.SaveChanges();
                }
            }
            return subdomain;
        }

        private Url ProcessLink(Uri link) {

            string domainName = GetDomainName(link);
            bool external = IsExternal(link);
            string subdomainName = link.Host.Replace(domainName, "").TrimEnd('.');

            Subdomain subdomain = ProcessSubdomain(subdomainName);
            Domain domain = ProcessDomain(domainName);

            Url url = new Url {
                ExternalUrl = external,
                Text = link.ToString(),
                Working = true,
                Subdomain = subdomain,
                Domain = domain,
                HashCode = link.GetHashCode().ToString("X8")
            };

            if (context.Urls.FirstOrDefault(x => x.HashCode == url.HashCode) != null) {
                url = context.Urls.First(x => x.HashCode == url.HashCode);
                url.Pages.Add(page);
                context.Entry(url).State = EntityState.Modified;
            } else {
                url.Pages.Add(page);
                context.Urls.Add(url);            
            }
            context.SaveChanges();
            return url;
        }

        public DatabaseSaver(string url, HtmlParser parser) {
            context = new CrawlerContext();
            this.parser = parser;
            mainUrl = url;          
        }

        public void ProcessPage(Uri startingUrl) {
            AddPage();
            if (!context.Urls.Any()) {
                Url url = ProcessLink(startingUrl);
                page.Urls.Add(url);
                context.SaveChanges();
            }
            //options.DownloadUri.ToString()
            foreach (Uri link in parser.GetLinks(startingUrl.ToString())) {
                Url url = ProcessLink(link);
                page.Urls.Add(url);
            }
            context.Entry(page).State = EntityState.Modified;
            context.SaveChanges();
        }

        public static void UpdateNonWorkingUrl(Uri uri, int errorCode) {
            CrawlerContext cnt = new CrawlerContext();
            string h = uri.GetHashCode().ToString("X8");
            Url url = cnt.Urls.First(u => u.HashCode == h);
            url.Working = false;
            url.ErrorCode = errorCode;
            cnt.Entry(url).State = EntityState.Modified;
            cnt.SaveChanges();
        }

        public bool IsExternal(Uri link) {
            string domainName = GetDomainName(link);
            return !domainName.Equals(GetDomainName(new Uri(mainUrl)));
        }
    }
}
