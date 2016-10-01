using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    #region Public test methods
    /// <summary>
    /// Check parser saves text to txt file.
    /// </summary>
    [Test]
    public void CanSaveText()
    {
      File.Delete(Constants.pathToEmptyDocText);
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToEmptyDoc);
      parser.SaveTextToFile(Constants.pathToEmptyDocText);

      Assert.IsTrue(File.Exists(Constants.pathToEmptyDocText), "TXT file with parsed text was not created.");
    }

    /// <summary>
    /// Parse empty docx with size = 0 (right click -> create new docx).
    /// </summary>
    [Test]
    public void Empty0SizeDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToEmpty0SizeDoc);

      Assert.AreEqual(Constants.emptyDocExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse empty doc with size !=0 (right click -> create new docx -> open -> save).
    /// </summary>
    [Test]
    public void OpenedEmptyDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToEmptyDoc);

      Assert.AreEqual(Constants.emptyDocExpectedResult +Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with .doc extension.
    /// </summary>
    [Test]
    public void DocDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToDocDoc);

      Assert.AreEqual(Constants.docDocExpectedResult + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with all English letters.
    /// </summary>
    [Test]
    public void EnglishLettersDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToEnglishLettersDoc);

      Assert.AreEqual(Constants.EnglishLettersExpectedResult + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with all numbers.
    /// </summary>
    [Test]
    public void NumbersDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToNumbersDoc);

      Assert.AreEqual(Constants.numbersExpectedResult + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with all Russian letters.
    /// </summary>
    [Test]
    public void RussianLettersDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToRussianLettersDoc);

      Assert.AreEqual(Constants.RussianLettersExpectedResult + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with special characters.
    /// </summary>
    [Test]
    public void SpecialCharactersDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToSpecialCharactersDoc);

      Assert.AreEqual(Constants.specialCharactersExpectedResult + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with columns.
    /// </summary>
    [Test]
    public void WithColumnsDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToWithColumnsDoc);

      Assert.AreEqual(Constants.withColumnsExpectedResult + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with links.
    /// </summary>
    [Test]
    public void WithLinkDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToWithLinkDoc);
      
      Assert.AreEqual(Constants.withLinkExpectedResult + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with contents.
    /// </summary>
    [Test]
    public void WithContentsDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToWithContentsDoc);

      Assert.AreEqual(Constants.withContentsExpectedResult2 + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with footnotes.
    /// </summary>
    [Test]
    public void WithFootnotesDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToWithFootnotesDoc);

      Assert.AreEqual(Constants.withFootnotesResult3 + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with formulas.
    /// </summary>
    [Test]
    public void WithFormulasDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToWithFormulasDoc);

      Assert.AreEqual(Constants.withFormulasExpectedResult1 + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with numeration.
    /// </summary>
    [Test]
    public void WithNumerationDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToWithNumerationDoc);
      
      Assert.AreEqual(Constants.withNumerationExpectedResult2 + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with picture.
    /// </summary>
    [Test]
    public void WithPictureDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToWithPictureDoc);
      
      Assert.AreEqual(Constants.withPictureExpectedResult + Constants.microsoftWordParserEnding + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with table.
    /// </summary>
    [Test]
    public void WithTableDoc()
    {
      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToWithTableDoc);

      Assert.AreEqual(Constants.withTableExpectedResult2 + Constants.microsoftWordParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse huge doc.
    /// </summary>
    [Test]
    [Ignore("Ignore hude doc")]
    public void HugeDoc()
    {
      File.Delete(Constants.pathToEmptyDocText);
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      Parser parser = new MicrosoftWordParser();
      parser.Parse(Constants.pathToHugeDocumentDoc);
      parser.SaveTextToFile(Constants.pathToEmptyDocText);
      stopwatch.Stop();

      Assert.Less(stopwatch.Elapsed, TimeSpan.FromSeconds(2));
    }
    #endregion
  }
}
