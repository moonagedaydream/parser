using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using ParserLibrary;
using ParserLibrary.DocParsers;

namespace ParserTest
{
  [TestFixture]
  public class NpoiDocParserTest
  {
    #region Public test methods
    /// <summary>
    /// Check parser saves text to txt file.
    /// </summary>
    [Test]
    public void CanSaveText()
    {
      File.Delete(Constants.pathToEmptyDocText);
      Parser parser = new NpoiDocParser();
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
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToEmpty0SizeDoc);

      Assert.AreEqual(Constants.emptyDocExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse empty doc with size !=0 (right click -> create new docx -> open -> save).
    /// </summary>
    [Test]
    public void OpenedEmptyDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToEmptyDoc);

      Assert.AreEqual(Constants.emptyDocExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with .doc extension.
    /// </summary>
    [Test]
    public void DocDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToDocDoc);

      Assert.AreEqual(Constants.docDocExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with all English letters.
    /// </summary>
    [Test]
    public void EnglishLettersDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToEnglishLettersDoc);

      Assert.AreEqual(Constants.EnglishLettersExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with all numbers.
    /// </summary>
    [Test]
    public void NumbersDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToNumbersDoc);

      Assert.AreEqual(Constants.numbersExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with all Russian letters.
    /// </summary>
    [Test]
    public void RussianLettersDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToRussianLettersDoc);

      Assert.AreEqual(Constants.RussianLettersExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with special characters.
    /// </summary>
    [Test]
    public void SpecialCharactersDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToSpecialCharactersDoc);

      Assert.AreEqual(Constants.specialCharactersExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with columns.
    /// </summary>
    [Test]
    public void WithColumnsDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToWithColumnsDoc);

      Assert.AreEqual(Constants.withColumnsExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with links.
    /// </summary>
    [Test]
    public void WithLinkDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToWithLinkDoc);

      Assert.AreEqual(Constants.withLinkExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with contents.
    /// </summary>
    [Test]
    public void WithContentsDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToWithContentsDoc);

      Assert.AreEqual(Constants.withContentsExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with footnotes.
    /// </summary>
    [Test]
    public void WithFootnotesDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToWithFootnotesDoc);

      Assert.AreEqual(Constants.withFootnotesResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with formulas.
    /// </summary>
    [Test]
    public void WithFormulasDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToWithFormulasDoc);

      Assert.AreEqual(Constants.withFormulasExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with numeration.
    /// </summary>
    [Test]
    public void WithNumerationDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToWithNumerationDoc);

      Assert.AreEqual(Constants.withNumerationExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with picture.
    /// </summary>
    [Test]
    public void WithPictureDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToWithPictureDoc);

      Assert.AreEqual(Constants.withPictureExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with table.
    /// </summary>
    [Test]
    public void WithTableDoc()
    {
      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToWithTableDoc);

      Assert.AreEqual(Constants.withTableExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse huge doc.
    /// </summary>
    [Test]
    public void HugeDoc()
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      Parser parser = new NpoiDocParser();
      parser.Parse(Constants.pathToHugeDocumentDoc);
      parser.SaveTextToFile(Constants.pathToEmptyDocText);
      stopwatch.Stop();

      Assert.Less(stopwatch.Elapsed, TimeSpan.FromSeconds(2));
    }
    #endregion
  }
}
