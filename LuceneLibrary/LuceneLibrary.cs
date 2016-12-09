using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;
using System.Linq;
using System.Text;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;
using ParserLibrary.HtmlParsers;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Documents;

namespace IndexLibrary
{
    public class Pages
    {
        public int Id { get; set; }
        public string content { get; set; }
        public string url { get; set; }
    }


    public class LuceneService 
    {
        private string indexPath = @"c:\temp\LuceneIndex";
        

        public List<Pages> GetFiles()
        {

            string url = "";
            string content = "";
            string id = "";
            var pages = new List<Pages>();
            var connectionString = ConfigurationManager.ConnectionStrings["CrawlerConnection"].ConnectionString;
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("SELECT u.Text, p.content,p.Id FROM Urls u INNER JOIN Pages p ON u.UrlId = p.MainUrl_UrlId", cnn);
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    url = dr.GetValue(0).ToString();
                    content = dr.GetValue(1).ToString();
                    id = dr.GetValue(2).ToString();
                    url = url.Substring(7, url.Length - 8);
                    /*string fileName = @"C:\temp1\" + id + @".txt";
                    using (System.IO.StreamWriter file =
                      new System.IO.StreamWriter(fileName))
                    {
                       file.WriteLine(content);
                     }*/
                    Pages tmp = new Pages();
                    tmp.content = content;
                    tmp.Id = Convert.ToInt32(id);
                    tmp.url = url;
                    pages.Add(tmp);
                }
            }
            cnn.Close();
            cnn.Dispose();
            return pages;

        }

        public void GetFilesWithParser()
        {
            string name = "";
            HtmlParser parser = new CsQueryParser();
            string[] dirs = System.IO.Directory.GetFiles(@"c:\temp\WebCrawlerPages", "*.html");
            for (int i = 0; i < dirs.Length; i++)
            {
                parser.Parse(dirs[i]);
                name = @"C:\temp2\"+ i + ".txt"; 
                parser.SaveTextToFile(name);
            }
        }

        public void CreateIndex(List<Pages> pages)
        {

            //var files = new List<string>();
            //foreach (var filePath in System.IO.Directory.EnumerateFiles(@"C:\temp2"))
            //{
            //files.Add(System.IO.File.ReadAllText(filePath));
            //}
            Directory luceneIndexDirectory = FSDirectory.Open(indexPath);
            Analyzer analyzer = new Lucene.Net.Analysis.Ru.RussianAnalyzer(Version.LUCENE_30);
            IndexWriter writer = new IndexWriter(luceneIndexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);

            // Lucene.Net.Documents.Document doc = new
            // Lucene.Net.Documents.Document();
            foreach (Pages p in pages)
            {
                Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();
                doc.Add(
                  new Lucene.Net.Documents.Field(
                    "id",
                    p.Id.ToString(),
                    Lucene.Net.Documents.Field.Store.YES,
                    Lucene.Net.Documents.Field.Index.NO,
                    Lucene.Net.Documents.Field.TermVector.NO));
                doc.Add(
                    new Lucene.Net.Documents.Field(
                        "url",
                        p.url,
                        Lucene.Net.Documents.Field.Store.YES,
                        Lucene.Net.Documents.Field.Index.NO,
                        Lucene.Net.Documents.Field.TermVector.NO));
                doc.Add(
                  new Lucene.Net.Documents.Field(
                    "text",
                   p.content,
                    Lucene.Net.Documents.Field.Store.YES,
                    Lucene.Net.Documents.Field.Index.ANALYZED,
                    Lucene.Net.Documents.Field.TermVector.YES));
                writer.AddDocument(doc);
                /* if (i % 100000 == 0)
                     Console.WriteLine(
                      "[{1}]: {0} documents are saved.",
                      i,
                      DateTime.Now);*/
            }
            //writer.Optimize();
            writer.Dispose();
            var indexReader = IndexReader.Open(luceneIndexDirectory, false);
           // var titleVector = indexReader.Terms();
           // while (titleVector.Next()) { Console.WriteLine(titleVector.Term.Text);}
        }
        private static Pages _mapLuceneDocumentToData(Document doc)
        {
            return new Pages
            {
                Id = Convert.ToInt32(doc.Get("Id")),
                url = doc.Get("url"),
                content = doc.Get("content")
            };
        }
        private static IEnumerable<Pages> _mapLuceneToDataList(IEnumerable<Document> hits)
        {
            return hits.Select(_mapLuceneDocumentToData).ToList();
        }
        private static IEnumerable<Pages> _mapLuceneToDataList(IEnumerable<ScoreDoc> hits,
            IndexSearcher searcher)
        {
            return hits.Select(hit => _mapLuceneDocumentToData(searcher.Doc(hit.Doc))).ToList();
        }
        public IEnumerable<Pages> Search(string q) {
            Analyzer analyzer = new Lucene.Net.Analysis.Ru.RussianAnalyzer(Version.LUCENE_30);
            Directory luceneIndexDirectory = FSDirectory.Open(indexPath);
            QueryParser parser = new QueryParser(Version.LUCENE_30, "text", analyzer);
            Query query = parser.Parse(q);
            var hits_limit = 100;
            IndexSearcher searcher = new IndexSearcher(luceneIndexDirectory);
            var hits = searcher.Search
            (query, null, hits_limit, Sort.RELEVANCE).ScoreDocs;

            var results = _mapLuceneToDataList(hits, searcher);
            analyzer.Close();
            searcher.Dispose();
            return results;
        }
       /* private  Pages _mapLuceneDocumentToData(Document doc)
        {
            return new Pages
            {
                Id = Convert.ToInt32(doc.Get("Id")),
                url = doc.Get("url"),
                content = doc.Get("content")
            };
        }
        private  IEnumerable<Pages> _mapLuceneToDataList(IEnumerable<Document> hits)
        {
            return hits.Select(_mapLuceneDocumentToData).ToList();
        }
        private  IEnumerable<Pages> _mapLuceneToDataList(IEnumerable<ScoreDoc> hits,
            IndexSearcher searcher)
        {
            return hits.Select(hit => _mapLuceneDocumentToData(searcher.Doc(hit.Doc))).ToList();
        }
        private  Query parseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }

        private  IEnumerable<Pages> _search (string searchQuery, string searchField = "")
        {
            Directory luceneIndexDirectory = FSDirectory.Open( @"c:\temp\LuceneIndex");
            // validation
            if (string.IsNullOrEmpty(searchQuery.Replace("*", "").Replace("?", ""))) return new List<Pages>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(luceneIndexDirectory, false))
            {
                var hits_limit = 1000;
                var analyzer = new Lucene.Net.Analysis.Ru.RussianAnalyzer(Version.LUCENE_30);

                // search by single field
                if (!string.IsNullOrEmpty(searchField))
                {
                    var parser = new QueryParser(Version.LUCENE_30, searchField, analyzer);
                    var query = parseQuery(searchQuery, parser);
                    var hits = searcher.Search(query, hits_limit).ScoreDocs;
                    var results = _mapLuceneToDataList(hits, searcher);
                    analyzer.Close();
                    searcher.Dispose();
                    return results;
                }
                // search by multiple fields (ordered by RELEVANCE)
                else
                {
                    var parser = new MultiFieldQueryParser
                        (Version.LUCENE_30, new[] { "Id", "url", "content" }, analyzer);
                    var query = parseQuery(searchQuery, parser);
                    var hits = searcher.Search
                    (query, null, hits_limit, Sort.RELEVANCE).ScoreDocs;
                    var results = _mapLuceneToDataList(hits, searcher);
                    analyzer.Close();
                    searcher.Dispose();
                    return results;
                }
            }
        }
        public  IEnumerable<Pages> Search(string input, string fieldName = "")
        {
            if (string.IsNullOrEmpty(input)) return new List<Pages>();

            var terms = input.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim() + "*");
            input = string.Join(" ", terms);

            return _search(input, fieldName);
        }
        public  IEnumerable<Pages> SearchDefault(string input, string fieldName = "")
        {
            return string.IsNullOrEmpty(input) ? new List<Pages>() : _search(input, fieldName);
        }
        public  IEnumerable<Pages> GetAllIndexRecords()
        {
            Directory luceneIndexDirectory = FSDirectory.Open(@"c:\temp\LuceneIndex");


            // set up lucene searcher
            var searcher = new IndexSearcher(luceneIndexDirectory, false);
            var reader = IndexReader.Open(luceneIndexDirectory, false);
            var docs = new List<Document>();
            var term = reader.TermDocs();
            while (term.Next()) docs.Add(searcher.Doc(term.Doc));
            reader.Dispose();
            searcher.Dispose();
            return _mapLuceneToDataList(docs);
        }*/
    }
}
