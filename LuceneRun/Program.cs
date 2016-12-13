using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using IndexLibrary;
namespace LuceneRun
{
    class Program
    {
        static void Main(string[] args)
        {

            LuceneService service = new LuceneService();
            List<Pages> p = service.GetFiles();
           // service.CreateIndex(p);
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            IEnumerable<Pages> tmp = service.Search("заявление");
            watch.Stop();
            int k = 0;
            foreach (Pages i in tmp)
            { Console.WriteLine(i.url); k++; }
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.WriteLine(k);
        }
    }
}
