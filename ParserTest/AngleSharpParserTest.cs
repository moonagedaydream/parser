using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using ParserLibrary;
using ParserLibrary.PdfParsers;
using ParserLibrary.HtmlParsers;

namespace ParserTest
{
  [TestFixture]
  public class AngleSharpParserTest
  {
    #region Public test methods
    /// <summary>
    /// Check parser saves text to txt file.
    /// </summary>
    [Test]
    public void CanSaveText()
    {
      File.Delete(Constants.pathToEmptyDocText);
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToAppliedMathematicMainPageHtml);
      parser.SaveTextToFile(Constants.pathToEmptyDocText);

      Assert.IsTrue(File.Exists(Constants.pathToEmptyDocText), "TXT file with parsed text was not created.");
    }

    /// <summary>
    /// Parse empty pdf with size !=0.
    /// </summary>
    [Test]
    public void ChineseSymbolsHtml()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToChineseSymbolsHtml);
      parser.SaveTextToFile(Constants.pathToEmptyDocText);
      Assert.AreEqual(Constants.emptyDocExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse pdf with all English letters.
    /// </summary>
    [Test]
    public void EnglishLettersPdf()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToEnglishLettersPdf);

      Assert.AreEqual(Constants.EnglishLettersExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with all numbers.
    /// </summary>
    [Test]
    public void NumbersPdf()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToNumbersPdf);

      Assert.AreEqual(Constants.numbersExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with all Russian letters.
    /// </summary>
    [Test]
    public void RussianLettersPdf()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToRussianLettersPdf);

      Assert.AreEqual(Constants.RussianLettersExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse doc with special characters.
    /// </summary>
    [Test]
    public void SpecialCharactersPdf()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToSpecialCharactersPdf);
      
      Assert.AreEqual(Constants.specialCharactersExpectedResultPdf, parser.Text);
    }

    /// <summary>
    /// Parse doc with columns.
    /// </summary>
    [Test]
    public void WithColumnsPdf()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToWithColumnsPdf);
      
      Assert.AreEqual(Constants.withColumnsExpectedResultPdf, parser.Text);
    }

    /// <summary>
    /// Parse doc with links.
    /// </summary>
    [Test]
    public void WithLinkPdf()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToWithLinkPdf);
      
      Assert.AreEqual(Constants.withLinkExpectedResulPdf2, parser.Text);
    }

    /// <summary>
    /// Parse doc with contents.
    /// </summary>
    [Test]
    public void WithContentsPdf()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToWithContentsPdf);
      
      Assert.AreEqual(Constants.withContentsExpectedResultPdf2, parser.Text);
    }

    /// <summary>
    /// Parse doc with footnotes.
    /// </summary>
    [Test]
    public void WithFootnotesPdf()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToWithFootnotesPdf);
      
      Assert.AreEqual(Constants.withFootnotesResultPdf, parser.Text);
    }

    /// <summary>
    /// Parse doc with formulas.
    /// </summary>
    [Test]
    public void WithFormulasPdf()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToWithFormulasPdf);

      Assert.AreEqual(Constants.withFormulasExpectedResult1, parser.Text);
    }

    /// <summary>
    /// Parse doc with numeration.
    /// </summary>
    [Test]
    public void WithNumerationPdf()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToWithNumerationPdf);
      
      Assert.AreEqual(Constants.withNumerationExpectedResultPdf2, parser.Text);
    }

    /// <summary>
    /// Parse doc with picture.
    /// </summary>
    [Test]
    public void WithPicturePdf()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToWithPicturePdf);
      
      Assert.AreEqual(Constants.withPictureExpectedResultPdf2, parser.Text);
    }

    /// <summary>
    /// Parse doc with table.
    /// </summary>
    [Test]
    public void WithTablePdf()
    {
      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToWithTablePdf);
      
      Assert.AreEqual(Constants.withTableExpectedResultPdf, parser.Text);
    }

    /// <summary>
    /// Parse huge doc.
    /// </summary>
    [Test]
    public void HugePdf()
    {
      File.Delete(Constants.pathToEmptyDocText);
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      Parser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToHugeDocumentPdf);
      parser.SaveTextToFile(Constants.pathToEmptyDocText);
      stopwatch.Stop();

      Assert.Less(stopwatch.Elapsed, TimeSpan.FromSeconds(20));
    }
    #endregion
  }
}
