using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace WebCrawlerLibrary
{
  class SpecializedWebCrawler : WebCrawler
  {
    private static readonly ILog log = LogManager.GetLogger("NCrawler");

    public SpecializedWebCrawler()
      : base()
      {
        XmlConfigurator.Configure();
        log.Debug("Crate Specialized Web Crawler.");

      }

      public override void Crawl(CancellationTokenSource cancellationTokenSource, string startUri)
      {
        //CrawlResult result = crawler.Crawl(new Uri(startUri), cancellationTokenSource);

        log.Info("=====================>>" + String.Join(", ", this.CrawledPages));
      }
  }
}
