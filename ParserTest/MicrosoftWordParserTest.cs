using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ParserLibrary;
using ParserLibrary.DocParsers;

namespace ParserTest
{
  [TestFixture]
  public class MicrosoftWordParserTest
  {
    #region Private fields
    private string pathToEmptyDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "Empty.docx");
    private string pathToEmptyDocText = Path.Combine(Environment.CurrentDirectory, @"ResultDocuments\", "Empty.docx");
    #endregion

    [Test]
    public void EmptyDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(pathToEmptyDoc);
      parser.SaveTextToFile(pathToEmptyDocText);

      string text = System.IO.File.ReadAllText(pathToEmptyDocText);

      Assert.AreEqual(string.Empty, text);
    }
  }
}
