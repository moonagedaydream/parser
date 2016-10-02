using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParserLibrary;
using ParserLibrary.DocParsers;
using ParserLibrary.HtmlParsers;
using ParserLibrary.PdfParsers;

namespace ParserRun {
    class Program {
        static void Main(string[] args) {
            try {
                //Parser parser = new HtmlAgilityPackParser();

                HtmlParser parser = new CsQueryParser();

                parser.Parse(@"C:\Users\Мария\Documents\Visual Studio 2013\Projects\ParserLibrary\ParserTest\HtmlTest\AppliedMathematicMainPage.html");//"C:\Users\Мария\Documents\test.html");
                //parser.BeautifyText();
                //parser.Parse(@"C:\Users\Мария\Documents\test.html");

                parser.SaveTextToFile(@"C:\Users\Мария\Documents\test.txt");
                parser.SaveAllLinksToFile(@"C:\Users\Мария\Documents\testlinks.txt");
                

            } catch (Exception e) {
                Console.WriteLine(e);
            }
            Console.ReadLine();

        }
    }
}
