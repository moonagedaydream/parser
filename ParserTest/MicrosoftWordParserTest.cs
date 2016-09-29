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
    private string pathToEmptyDocText = Path.Combine(Environment.CurrentDirectory, @"ResultDocuments\", "Empty.txt");
    private string pathToEmpty0SizeDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "Empty0Size.docx");
    #endregion

    #region Public test methods
    /// <summary>
    /// Check parser saves text to txt file.
    /// </summary>
    [Test]
    public void CanSaveText()
    {
      File.Delete(pathToEmptyDocText);
      Parser parser = new MicrosoftWordParser();
      parser.Parse(pathToEmptyDoc);
      parser.SaveTextToFile(pathToEmptyDocText);

      Assert.IsTrue(File.Exists(pathToEmptyDocText), "TXT file with parsed text was not created.");
    }

    /// <summary>
    /// Parse empty docx with size = 0 (right click -> create new docx).
    /// </summary>
    [Test]
    public void Empty0SizeDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(pathToEmpty0SizeDoc);

      Assert.AreEqual(string.Empty, parser.Text);
    }

    /// <summary>
    /// Parse empty doc with size !=0 (right click -> create new docx -> open -> save).
    /// </summary>
    [Test]
    public void OpenedEmptyDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(pathToEmptyDoc);

      Assert.AreEqual(string.Empty, parser.Text);
    }
    #endregion
  }
}
