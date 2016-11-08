using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CsQuery.ExtensionMethods;
using log4net;
using WebCrawlerLibrary.SpecializedWebCrawlerHelper.Models;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper {
    public class StatisticsInfo {

        private CrawlerContext context;
        private static readonly ILog log = LogManager.GetLogger("WebCrawler");

        private int numberOfPages;
        private int numberOfUrls;
        private long totalSizeofPages;
        private int numberOfBrokenUrls;
        private int numberOfExternalLinks;
        private int numberOfInternalSubdomains;
        private IList<Subdomain> internalSubdomains;
        private long totalElapsedMilliseconds = 0;

        private void GetStatistics(long? elapsedMilliseconds = null, string domainName = null) {

            log.InfoFormat("Getting statistics.");
            if (!string.IsNullOrEmpty(domainName)) {
                numberOfPages = context.Pages.Count(p => p.MainUrl.Domain.Name.Equals(domainName));
                numberOfUrls = context.Urls.Count(u => u.BaseDomain.Name.Equals(domainName));
                totalSizeofPages = context.Pages.Where(p => p.MainUrl.Domain.Name.Equals(domainName)).Sum(p => p.Size);
                numberOfBrokenUrls = context.Urls.Where(u => u.BaseDomain.Name.Equals(domainName)).Count(u => !u.Working);
                numberOfExternalLinks = context.Urls.Where(u => u.BaseDomain.Name.Equals(domainName)).Count(u => u.ExternalUrl);
                internalSubdomains = context.Subdomains.Where(s => s.Domain.Name.Equals(domainName)).ToList();
                numberOfInternalSubdomains = internalSubdomains.Count(s => s.Domain.Name.Equals(domainName));
            } else {
                numberOfPages = context.Pages.Count();
                numberOfUrls = context.Urls.Count();
                totalSizeofPages = context.Pages.Sum(p => p.Size);
                numberOfBrokenUrls = context.Urls.Count(u => !u.Working);
                numberOfExternalLinks = context.Urls.Count(u => u.ExternalUrl);
                internalSubdomains = context.Subdomains.ToList();
                numberOfInternalSubdomains = internalSubdomains.Count();
            }
            if (elapsedMilliseconds != null && !string.IsNullOrEmpty(domainName)) {
                Time time = context.Times.FirstOrDefault(t => t.Domain.Name.Equals(domainName));
                long m = 0;
                if (time == null) {
                    Domain domain = context.Domains.FirstOrDefault(d => d.Name.Equals(domainName));
                    if (domain != null) {
                        context.Times.Add(new Time { Milliseconds = elapsedMilliseconds.Value, Domain = domain });
                        context.SaveChanges();
                        m = elapsedMilliseconds.Value;
                    }
                } else {
                    m = time.Milliseconds + elapsedMilliseconds.Value;
                    time.Milliseconds += elapsedMilliseconds.Value;
                    context.Entry(time).State = EntityState.Modified;
                    context.SaveChanges();
                }
                totalElapsedMilliseconds = m;
            }

            log.InfoFormat("Statistics counted.");
            log.InfoFormat("Total Number Of Pages: {0}.", numberOfPages);
            log.InfoFormat("Total Size Of Pages: {0} bytes.", totalSizeofPages);
            log.InfoFormat("Total Number Of Urls: {0}.", numberOfUrls);
            log.InfoFormat("Number Of Broken Urls: {0}.", numberOfBrokenUrls);
            log.InfoFormat("Number Of External Links: {0}.", numberOfExternalLinks);
            log.InfoFormat("Number Of Internal Subdomains: {0}.", numberOfInternalSubdomains);
            log.InfoFormat("Internal Subdomains: ");
            InternalSubdomains.ForEach(s => log.InfoFormat(" {0},", s.Name));
            if (totalElapsedMilliseconds != 0) {
                TimeSpan t = TimeSpan.FromMilliseconds(totalElapsedMilliseconds);
                string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                        t.Hours,
                                        t.Minutes,
                                        t.Seconds,
                                        t.Milliseconds);
                log.InfoFormat("Total Elapsed time: {0}.", answer);
            }

        }

        public int NumberOfPages {
            get { return numberOfPages; }
        }
        public int NumberOfUrls {
            get { return numberOfUrls; }
        }
        public long TotalSizeofPages {
            get { return totalSizeofPages; }
        }
        public int NumberOfBrokenUrls {
            get { return numberOfBrokenUrls; }
        }
        public int NumberOfExternalLinks {
            get { return numberOfExternalLinks; }
        }
        public int NumberOfInternalSubdomains {
            get { return numberOfInternalSubdomains; }
        }
        public IList<Subdomain> InternalSubdomains {
            get { return internalSubdomains; }
        }
        public long TotalElapsedMilliseconds {
            get { return totalElapsedMilliseconds; }
        }

        public StatisticsInfo() {
            context = new CrawlerContext();
            GetStatistics();
        }

        public StatisticsInfo(long? elapsedMilliseconds = null, Uri url = null) {
            context = new CrawlerContext();
            GetStatistics(elapsedMilliseconds, url == null ? null : DatabaseSaver.GetDomainName(url));
        }

        public void Print() {
            Console.WriteLine("=====================================");
            Console.WriteLine("STATISTICS");
            Console.WriteLine("=====================================");
            Console.WriteLine("Total Number Of Pages: {0}.", numberOfPages);
            Console.WriteLine("Total Size Of Pages: {0} bytes.", totalSizeofPages);
            Console.WriteLine("Total Number Of Urls: {0}.", numberOfUrls);
            Console.WriteLine("Number Of Broken Urls: {0}.", numberOfBrokenUrls);
            Console.WriteLine("Number Of External Links: {0}.", numberOfExternalLinks);
            Console.WriteLine("Number Of Internal Subdomains: {0}.", numberOfInternalSubdomains);
            Console.Write("Internal Subdomains: ");
            InternalSubdomains.ForEach(s => Console.Write(" {0},", s.Name));
            Console.WriteLine();
            if (totalElapsedMilliseconds != 0) {
                TimeSpan t = TimeSpan.FromMilliseconds(totalElapsedMilliseconds);
                string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                        t.Hours,
                                        t.Minutes,
                                        t.Seconds,
                                        t.Milliseconds);
                Console.WriteLine("Total Elapsed time: {0}.", answer);
            }
            Console.WriteLine("=====================================");
        }

        public void PrintToFile(string startUri)
        {
            string fileName = @"C:\temp\" + startUri + @"Statistics.txt";
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fileName))
            {
                file.WriteLine("=====================================");
                file.WriteLine("STATISTICS");
                file.WriteLine("=====================================");
                file.WriteLine("Total Number Of Pages: {0}.", numberOfPages);
                file.WriteLine("Total Size Of Pages: {0} bytes.", totalSizeofPages);
                file.WriteLine("Total Number Of Urls: {0}.", numberOfUrls);
                file.WriteLine("Number Of Broken Urls: {0}.", numberOfBrokenUrls);
                file.WriteLine("Number Of External Links: {0}.", numberOfExternalLinks);
                file.WriteLine("Number Of Internal Subdomains: {0}.", numberOfInternalSubdomains);
                file.Write("Internal Subdomains: ");
                InternalSubdomains.ForEach(s => file.Write(" {0},", s.Name));
                file.WriteLine();
                if (totalElapsedMilliseconds != 0)
                {
                    TimeSpan t = TimeSpan.FromMilliseconds(totalElapsedMilliseconds);
                    string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                            t.Hours,
                                            t.Minutes,
                                            t.Seconds,
                                            t.Milliseconds);
                    file.WriteLine("Total Elapsed time: {0}.", answer);
                }
                file.WriteLine("=====================================");
            }
        }
    }
}
