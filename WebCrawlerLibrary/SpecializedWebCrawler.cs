using System;
using System.IO;
using System.Threading;
using log4net;
using log4net.Config;
using WebCrawlerLibrary.SpecializedWebCrawlerHelper;

namespace WebCrawlerLibrary
{
  public class SpecializedWebCrawler : WebCrawler
  {
    private static readonly ILog log = LogManager.GetLogger("WebCrawler");

    public SpecializedWebCrawler()
      : base()
      {
        XmlConfigurator.Configure();
        log.Debug("Create Specialized Web Crawler.");
      }

    public override void Crawl(string startUri)
    {
      var options =
         new CrawlerOptions
         {
           DownloadUri = new Uri(startUri),
           DestinationFolderPath = new DirectoryInfo(@"C:\temp\WebCrawlerPages")
         };

      var crawler = new PagesDownloader(options);

      crawler.ProcessAsync();
    }

      public override void Crawl(CancellationTokenSource cancellationTokenSource, string startUri)
      {
        Crawl(startUri);
      }
  }
}
