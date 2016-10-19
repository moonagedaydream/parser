using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NCrawler;
using log4net;
using log4net.Config;
using NCrawler.Extensions;
using NCrawler.Interfaces;
using ILog = log4net.ILog;
using NCrawler.Extensions;
using NCrawler.HtmlProcessor;
using NCrawler.Interfaces;

namespace WebCrawlerLibrary
{
    public class NCrawler: WebCrawler
    {
      private static readonly ILog log = LogManager.GetLogger("NCrawler");
      private Crawler crawler;

      #region Nested type: DumperStep

      /// <summary>
      /// 	Custom pipeline step, to dump url to console
      /// </summary>
      internal class DumperStep : IPipelineStep
      {
        #region IPipelineStep Members

        /// <summary>
        /// </summary>
        /// <param name = "crawler">
        /// 	The crawler.
        /// </param>
        /// <param name = "propertyBag">
        /// 	The property bag.
        /// </param>
        public void Process(Crawler crawler, PropertyBag propertyBag)
        {
          CultureInfo contentCulture = (CultureInfo)propertyBag["LanguageCulture"].Value;
          string cultureDisplayValue = "N/A";
          if (!contentCulture.IsNull())
          {
            cultureDisplayValue = contentCulture.DisplayName;
          }

          lock (this)
          {
            log.InfoFormat("!!!Url: {0}", propertyBag.Step.Uri);
            //((WebCrawler)crawler)
            //Console.Out.WriteLine(ConsoleColor.Gray, "Url: {0}", propertyBag.Step.Uri);
            //Console.Out.WriteLine(ConsoleColor.DarkGreen, "\tContent type: {0}", propertyBag.ContentType);
            //Console.Out.WriteLine(ConsoleColor.DarkGreen, "\tContent length: {0}",
            //  propertyBag.Text.IsNull() ? 0 : propertyBag.Text.Length);
            //Console.Out.WriteLine(ConsoleColor.DarkGreen, "\tDepth: {0}", propertyBag.Step.Depth);
            //Console.Out.WriteLine(ConsoleColor.DarkGreen, "\tCulture: {0}", cultureDisplayValue);
            //Console.Out.WriteLine(ConsoleColor.DarkGreen, "\tThreadId: {0}", Thread.CurrentThread.ManagedThreadId);
            //Console.Out.WriteLine(ConsoleColor.DarkGreen, "\tThread Count: {0}", crawler.ThreadsInUse);
            //Console.Out.WriteLine();
          }
        }

        #endregion
      }

      #endregion

      public NCrawler()
        : base()
      {
        XmlConfigurator.Configure();
        log.Debug("Crate NCrawler.");
        
      }

      public override void Crawl(CancellationTokenSource cancellationTokenSource, string startUri)
      {
        //crawler = new Crawler(new Uri(startUri));
        log.Info("=====================>>1" + String.Join(", ", this.CrawledPages));
        crawler = new Crawler(new Uri(startUri),
        new HtmlDocumentProcessor(), // Process html
        new DumperStep())
				{
					// Custom step to visualize crawl
					MaximumThreadCount = 2,
					MaximumCrawlDepth = 10
				};
        log.Info("=====================>>2" + String.Join(", ", this.CrawledPages));
        crawler.Crawl();
        log.Info("=====================>>" + String.Join(", ", this.CrawledPages));
      }
    }
}
