using System;
using System.Data.Entity;
using System.Linq;
using log4net;
using ParserLibrary.HtmlParsers;
using WebCrawlerLibrary.SpecializedWebCrawlerHelper.Models;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper {
    class DatabaseSaver {

        private static readonly ILog log = LogManager.GetLogger("WebCrawler");
        private readonly string mainUrl;
        private readonly CrawlerContext context;
        private readonly HtmlParser parser;
        private Page page;

        private void AddPage(Url link) {
            page = new Page {
                Content = parser.Text,
                Size = Convert.ToInt32(parser.Size / 1024),
                MainUrl = link
            };

            context.Pages.Add(page);
            context.SaveChanges();
        }

        public static string GetDomainName(Uri link) {
            string[] hostParts = link.Host.Split('.');
            return String.Join(".", hostParts.Skip(Math.Max(0, hostParts.Length - 2)).Take(2));
        }

        private Domain ProcessDomain(string domainName) {

            log.InfoFormat("Processing domain with name '{0}'.", domainName);

            Domain domain = null;
            domain = context.Domains.FirstOrDefault(d => d.Name.Equals(domainName));
            if (domain == null) {

                log.InfoFormat("Saving domain with name '{0}' to database.", domainName);

                domain = new Domain { Name = domainName };
                context.Domains.Add(domain);
                context.SaveChanges();
            } else {
                log.InfoFormat("Domain with name '{0}' is already exist.", domainName);
            }
            return domain;
        }

        private Subdomain ProcessSubdomain(string subdomainName) {

            log.InfoFormat("Processing internal subdomain with name '{0}'.", subdomainName);

            Subdomain subdomain = null;
            if (!string.IsNullOrEmpty(subdomainName)) {
                subdomain = context.Subdomains.FirstOrDefault(s => s.Name.Equals(subdomainName));
                if (subdomain == null) {

                    log.InfoFormat("Saving internal subdomain with name '{0}' to database.", subdomainName);

                    subdomain = new Subdomain { Name = subdomainName, Domain = page.MainUrl.BaseDomain};
                    context.Subdomains.Add(subdomain);
                    context.SaveChanges();
                } else {
                    log.InfoFormat("Internal subdomain with name '{0}' is already exist.", subdomainName);
                }
            }
            return subdomain;
        }

        private Url CreateUrl(Uri link) {
            string domainName = GetDomainName(link);
            bool external = IsExternal(link);
            string subdomainName = link.Host.Replace(domainName, "").TrimEnd('.');

            Subdomain subdomain = null;
            if (!external && !string.IsNullOrWhiteSpace(subdomainName) && !subdomainName.Equals("www")) {
                subdomain = ProcessSubdomain(subdomainName);
            }
            Domain domain = null;
            if (!external) {
                domain = ProcessDomain(domainName);
            }

            return new Url {
                ExternalUrl = external,
                Text = link.ToString(),
                Working = true,
                Subdomain = subdomain,
                Domain = domain,
                BaseDomain = page == null ? domain : page.MainUrl.Domain,
                HashCode = link.GetHashCode().ToString("X8")
            };
        }

        private Url ProcessLink(Uri link) {

            log.InfoFormat("Saving information about URI '{0}' to database.", link);

            Url url = CreateUrl(link);

            if (context.Urls.FirstOrDefault(x => x.HashCode == url.HashCode) != null) {

                log.InfoFormat("URI '{0}' is already exist.", link);

                url = context.Urls.First(x => x.HashCode == url.HashCode);
                url.Pages.Add(page);
                context.Entry(url).State = EntityState.Modified;
            } else {

                log.InfoFormat("Saving information about URI '{0}' to database.", link);

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

            log.InfoFormat("Saving page with URI '{0}' to database.", startingUrl);

            Url selfUrl = CreateUrl(startingUrl);
            if (context.Urls.FirstOrDefault(x => x.HashCode == selfUrl.HashCode) != null) {
                selfUrl = context.Urls.First(x => x.HashCode == selfUrl.HashCode);
            }

            AddPage(selfUrl);

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

            log.InfoFormat("Saving info about non working URI '{0}' to database. ErrorCode: '{1}'", uri, errorCode);

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
