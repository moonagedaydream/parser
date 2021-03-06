﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WebCrawlerLibrary;
using System.Data.SqlClient;
using System.Data;
using WebCrawlerLibrary.SpecializedWebCrawlerHelper;
using System.Diagnostics;
using System.Configuration;
using System.Threading;

namespace WebCrawlerTest
{
    [TestFixture]
    public class WebCrawlerTest
    {
        [Test]

        public void CanCrowlUrls()

        {
            Int32 countUrls = 0;
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");
            watch.Stop();
            Thread.Sleep(15000);
            StatisticsInfo statistics = new StatisticsInfo(
              watch.ElapsedMilliseconds,
              new Uri(@"http://0va.ru/"));
            countUrls = statistics.NumberOfUrls;
            Assert.AreEqual(4, countUrls);

        }

        [Test]


        public void CanCrowlExternal()

        {
            Int32 countUrls = 0;
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");
            watch.Stop();
            Thread.Sleep(15000);
            StatisticsInfo statistics = new StatisticsInfo(
              watch.ElapsedMilliseconds,
              new Uri(@"http://0va.ru/"));
            countUrls = statistics.NumberOfExternalLinks;
            Assert.AreEqual(3, countUrls);

        }

        [Test]
        public void CanGetContent()

        {
            string content = "";
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");
            var connectionString = ConfigurationManager.ConnectionStrings["GetContentConnection"].ConnectionString;
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Select Content From Pages Where id=1", cnn);
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    content = dr.GetValue(0).ToString();
                }
            }
            cnn.Close();
            cnn.Dispose();
            string realContent = "Идея создать самый простой сайт в интернете родилась спонтанно, и через 10 минут она была реализована (а еще через 10 сайт и сервер были отмизированы и теперь этот сайт самый быстрый сайт в мире Pingdom.com говорит \"Your website is faster than 100% of all tested websites\") - результат перед Вами  Самый быстрый результат загрузки сайта составил 28ms (1/36 секунды) МИРОВОЙ РЕКОРД НЕ ИНАЧЕ:))\r\nВ то же время даже такой простой сайт на котором есть заголовок, пара предложений и одна ссылка : несет в себе глубокий филосовский смысл \"этот простой сайт, а смысла в нем как в целом интернете - немного информации и ссылка на подробную информацию\".\r\nЗ.Ы. Одна из сылок ведет на сайт со смыслом, в интернете одно говно все знают.\r\n";
            Assert.AreEqual(realContent, content);

        }

        
        [Test]
        public void CanCrowlSubdomains()

        {
            int countSubdomains = 0;
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");
            watch.Stop();
            Thread.Sleep(15000);
            StatisticsInfo statistics = new StatisticsInfo(
              watch.ElapsedMilliseconds,
              new Uri(@"http://0va.ru/"));
            countSubdomains = statistics.NumberOfInternalSubdomains;
            Assert.AreEqual(0, countSubdomains);

        }

        [Test]
        public void CanCrowlErrorUrls()

        {
            int countErrorUrls = 0;
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");
            watch.Stop();
            Thread.Sleep(30000);
            StatisticsInfo statistics = new StatisticsInfo(
              watch.ElapsedMilliseconds,
              new Uri(@"http://0va.ru/"));
            countErrorUrls = statistics.NumberOfBrokenUrls;
            Assert.AreEqual(0, countErrorUrls);

        }

        [Test]

        public void CanCrowlPages()

        {
            Int32 countPages = 0;
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");
            watch.Stop();
            Thread.Sleep(30000);
            StatisticsInfo statistics = new StatisticsInfo(
              watch.ElapsedMilliseconds,
              new Uri(@"http://0va.ru/"));
            countPages = statistics.NumberOfPages;
            Assert.AreEqual(1, countPages);

        }

        [Test]
        public void timeTestUri()
        {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");

            stopwatch.Stop();

            Assert.Less(stopwatch.Elapsed, TimeSpan.FromSeconds(60));
        }

        /* [Test]
         public void timeSpbu()
         {

             Stopwatch stopwatch = new Stopwatch();
             stopwatch.Start();
             WebCrawler crawler = new SpecializedWebCrawler();
             crawler.Crawl(@"http://spbu.ru/");

             stopwatch.Stop();

             Assert.Less(stopwatch.Elapsed, TimeSpan.FromMinutes(30));
         }
         [Test]
         public void timeMsu()
         {

             Stopwatch stopwatch = new Stopwatch();
             stopwatch.Start();
             WebCrawler crawler = new SpecializedWebCrawler();
             crawler.Crawl(@"http://msu.ru/");

             stopwatch.Stop();

             Assert.Less(stopwatch.Elapsed, TimeSpan.FromMinutes(30));
         }*/
    }
}

