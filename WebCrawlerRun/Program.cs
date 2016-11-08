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

      System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
      
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
        Task.Factory.StartNew(() =>
        { crawler.Crawl(@"http://spbu.ru/"); }
          , cancellationTokenSource.Token);
        //Task.Factory.StartNew(() =>
        //{ crawler.Crawl(@"http://spbu.ru/"); }
        //  , cancellationTokenSource.Token);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      } 
      finally
      {
        watch.Stop();
      }

      try {
          StatisticsInfo statistics = new StatisticsInfo(
              watch.ElapsedMilliseconds, 
              new Uri(@"http://spbu.ru/"));
          statistics.Print();

      } catch (Exception e) {
          Console.WriteLine(e);
      }

      Console.WriteLine("Press any key to exit.");
      Console.ReadLine();
    }
  }
}
