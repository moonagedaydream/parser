using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParserLibrary;
using ParserLibrary.DocParsers;
using ParserLibrary.PdfParsers;

namespace ParserRun {
    class Program {
        static void Main(string[] args) {
            try {
                Parser parser = new MicrosoftWordParser();
                parser.Parse(@"C:\Users\Мария\Downloads\Zdravstvuyte.docx");

                parser.SaveTextToFile(@"C:\Users\Мария\Documents\test.txt");

            } catch (Exception e) {
                Console.WriteLine(e);
            }
            Console.ReadLine();

        }
    }
}
