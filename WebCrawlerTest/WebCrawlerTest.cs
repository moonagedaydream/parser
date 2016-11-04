using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WebCrawlerLibrary;
using System.Data.SqlClient;
using System.Data;


namespace WebCrawlerTest
{
    [TestFixture]
    public class WebCrawlerTest
    {
        [Test]

        public void CanCrowlUrls()

        {
            Int32 countUrls = 0;
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");
            SqlConnection cnn = new SqlConnection(@"Data Source=OLGA-PC;Initial Catalog=CrawlerDB;Integrated Security=True");
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Select Count(*) From Urls", cnn);
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    countUrls = Convert.ToInt32(dr.GetValue(0).ToString());
                }
            }
            cnn.Close();
            cnn.Dispose();
            Assert.AreEqual(4, countUrls);

        }

        [Test]


        public void CanCrowlExternal()

        {
            Int32 countUrls = 0;
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");
            SqlConnection cnn = new SqlConnection(@"Data Source=OLGA-PC;Initial Catalog=CrawlerDB;Integrated Security=True");
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Select Count(*) From Urls Where ExternalUrl=1", cnn);
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    countUrls = Convert.ToInt32(dr.GetValue(0).ToString());
                }
            }
            cnn.Close();
            cnn.Dispose();
            Assert.AreEqual(3, countUrls);

        }
        public void CanGetContent()

        {
            string content = "";
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");
            SqlConnection cnn = new SqlConnection(@"Data Source=OLGA-PC;Initial Catalog=CrawlerDB;Integrated Security=True");
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
            string realContent = "Идея создать самый простой сайт в интернете родилась спонтанно, и через 10 минут она была реализована (а еще через 10 сайт и сервер были отмизированы и теперь этот сайт самый быстрый сайт в мире Pingdom.com говорит \"Your website is faster than 100 % of all tested websites\") - результат перед Вами  Самый быстрый результат загрузки сайта составил 28ms (1/36 секунды) МИРОВОЙ РЕКОРД НЕ ИНАЧЕ:)) В то же время даже такой простой сайт на котором есть заголовок, пара предложений и одна ссылка : несет в себе глубокий филосовский смысл \"этот простой сайт, а смысла в нем как в целом интернете - немного информации и ссылка на подробную информацию\".З.Ы.Одна из сылок ведет на сайт со смыслом, в интернете одно говно все знают.";
            Assert.AreEqual(realContent, content);

        }

        [Test]
        public void CanCrowlSubdomains()

        {
            int countSubdomains = 0;
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");
            SqlConnection cnn = new SqlConnection(@"Data Source=OLGA-PC;Initial Catalog=CrawlerDB;Integrated Security=True");
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Select Count(*) From Subdomains", cnn);
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    countSubdomains = Convert.ToInt32(dr.GetValue(0).ToString());
                }
            }
            cnn.Close();
            cnn.Dispose();
            Assert.AreEqual(0, countSubdomains);

        }

        [Test]
        public void CanCrowlErrorUrls()

        {
            int countErrorUrls = 0;
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");
            SqlConnection cnn = new SqlConnection(@"Data Source=OLGA-PC;Initial Catalog=CrawlerDB;Integrated Security=True");
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Select Count(*) From Urls Where ErrorCode is not null", cnn);
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    countErrorUrls = Convert.ToInt32(dr.GetValue(0).ToString());
                }
            }
            cnn.Close();
            cnn.Dispose();
            Assert.AreEqual(0, countErrorUrls);

        }

        [Test]

        public void CanCrowlPages()

        {
            Int32 countPages = 0;
            WebCrawler crawler = new SpecializedWebCrawler();
            crawler.Crawl(@"http://0va.ru/");
            SqlConnection cnn = new SqlConnection(@"Data Source=OLGA-PC;Initial Catalog=CrawlerDB;Integrated Security=True");
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Select Count(*) From Pages", cnn);
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    countPages = Convert.ToInt32(dr.GetValue(0).ToString());
                }
            }
            cnn.Close();
            cnn.Dispose();
            Assert.AreEqual(1, countPages);

        }
    }
}
