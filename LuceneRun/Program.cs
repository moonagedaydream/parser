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
            service.CreateIndex(p);
            IEnumerable<Pages> tmp = service.Search("заявление");
            foreach(Pages i in tmp)
            Console.WriteLine(i.url);
        }
    }
}
