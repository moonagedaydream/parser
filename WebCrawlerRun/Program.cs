using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebCrawlerLibrary;
using WebCrawlerLibrary.SpecializedWebCrawlerHelper;

namespace WebCrawlerRun
{
  class Program
  {
    static void Main(string[] args)
    {
      var cancellationTokenSource = new CancellationTokenSource();
      try
      {
        Task.Factory.StartNew(() =>
        {
          while (!cancellationTokenSource.IsCancellationRequested)
          {
            char ch = Console.ReadKey().KeyChar;
            if (ch == 'c' || ch == 'C')
            {
              cancellationTokenSource.Cancel();
            }
          }
        }
        );

        WebCrawler crawler = new SpecializedWebCrawler();
        crawler.Crawl(@"http://spbu.ru/");
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
      Console.WriteLine("Press any key to exit.");
      Console.ReadLine();
    }
  }
}
