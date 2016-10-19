using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abot.Crawler;
using Abot.Poco;
using log4net;
using log4net.Config;

namespace WebCrawlerLibrary
{
    public class AbotWebCrawler: WebCrawler
    {
      private static readonly ILog log = LogManager.GetLogger("AbotWebCrawler");
      private readonly PoliteWebCrawler crawler;

      #region Private methods
      private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
      {
        PageToCrawl pageToCrawl = e.PageToCrawl;
        this.CrawledPages.Add(pageToCrawl.Uri.AbsoluteUri);

        log.InfoFormat("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
      }

      private void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
      {
        CrawledPage crawledPage = e.CrawledPage;

        if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
          log.InfoFormat("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
        else
          log.InfoFormat("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

        if (string.IsNullOrEmpty(crawledPage.Content.Text))
          log.InfoFormat("Page had no content {0}", crawledPage.Uri.AbsoluteUri);

        var htmlAgilityPackDocument = crawledPage.HtmlDocument; //Html Agility Pack parser
        var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument; //AngleSharp parser
      }

      private void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
      {
        CrawledPage crawledPage = e.CrawledPage;
        log.InfoFormat("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri, e.DisallowedReason);
      }

      private void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
      {
        PageToCrawl pageToCrawl = e.PageToCrawl;
        log.InfoFormat("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason);
      }
      #endregion

      public AbotWebCrawler(): base()
      {
        XmlConfigurator.Configure();
        log.Debug("Crate AbotWebCrawler.");
        crawler = new PoliteWebCrawler();
        
        crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
        crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
        crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
        crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;
      }

      public override void Crawl(CancellationTokenSource cancellationTokenSource, string startUri)
      {
        CrawlResult result = crawler.Crawl(new Uri(startUri), cancellationTokenSource);

        log.Info("=====================>>" + String.Join(", ", this.CrawledPages));
      }


    }
}
