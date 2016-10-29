using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebCrawlerLibrary
{
  public abstract class WebCrawler
  {
    // Run crawler.
    public abstract void Crawl(CancellationTokenSource cancellationTokenSource, string startUri);

    // Run crawler.
    public abstract void Crawl(string startUri);

    // Processed pages.
    public List<string> CrawledPages = new List<string>();
  }
}
