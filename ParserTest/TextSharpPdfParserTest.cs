using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using ParserLibrary;
using ParserLibrary.PdfParsers;
using ParserLibrary.PdfParsers;

namespace ParserTest
{
  [TestFixture]
  public class TextSharpPdfParserTest
  {
    #region Public test methods
    /// <summary>
    /// Check parser saves text to txt file.
    /// </summary>
    [Test]
    public void CanSaveText()
    {
      File.Delete(Constants.pathToEmptyDocText);
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToEmptyPdf);
      parser.SaveTextToFile(Constants.pathToEmptyDocText);

      Assert.IsTrue(File.Exists(Constants.pathToEmptyDocText), "TXT file with parsed text was not created.");
    }

    /// <summary>
    /// Parse empty pdf with size !=0.
    /// </summary>
    [Test]
    public void OpenedEmptyPdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToEmptyPdf);

      Assert.AreEqual(Constants.emptyDocExpectedResult + Constants.textSharpPdfParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse pdf with all English letters.
    /// </summary>
    [Test]
    public void EnglishLettersPdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToEnglishLettersPdf);

      Assert.AreEqual(Constants.EnglishLettersExpectedResult + Constants.textSharpPdfParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with all numbers.
    /// </summary>
    [Test]
    public void NumbersPdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToNumbersPdf);

      Assert.AreEqual(Constants.numbersExpectedResult + Constants.textSharpPdfParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with all Russian letters.
    /// </summary>
    [Test]
    public void RussianLettersPdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToRussianLettersPdf);

      Assert.AreEqual(Constants.RussianLettersExpectedResult + Constants.textSharpPdfParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with special characters.
    /// </summary>
    [Test]
    public void SpecialCharactersPdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToSpecialCharactersPdf);
      
      Assert.AreEqual(Constants.specialCharactersExpectedResultPdf2, parser.Text);
    }

    /// <summary>
    /// Parse doc with columns.
    /// </summary>
    [Test]
    public void WithColumnsPdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToWithColumnsPdf);
      
      Assert.AreEqual(Constants.withColumnsExpectedResult + Constants.textSharpPdfParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with links.
    /// </summary>
    [Test]
    public void WithLinkPdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToWithLinkPdf);
      
      Assert.AreEqual(Constants.withLinkExpectedResulPdf + Constants.textSharpPdfParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with contents.
    /// </summary>
    [Test]
    public void WithContentsPdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToWithContentsPdf);
      
      Assert.AreEqual(Constants.withContentsExpectedResultPdf + Constants.textSharpPdfParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with footnotes.
    /// </summary>
    [Test]
    public void WithFootnotesPdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToWithFootnotesPdf);

      Assert.AreEqual(Constants.withFootnotesResult + Constants.textSharpPdfParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with formulas.
    /// </summary>
    [Test]
    public void WithFormulasPdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToWithFormulasPdf);

      Assert.AreEqual(Constants.withFormulasExpectedResult1 + Constants.textSharpPdfParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with numeration.
    /// </summary>
    [Test]
    public void WithNumerationPdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToWithNumerationPdf);
      
      Assert.AreEqual(Constants.withNumerationExpectedResultPdf + Constants.textSharpPdfParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with picture.
    /// </summary>
    [Test]
    public void WithPicturePdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToWithPicturePdf);
      
      Assert.AreEqual(Constants.withPictureExpectedResultPdf + Constants.textSharpPdfParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse doc with table.
    /// </summary>
    [Test]
    public void WithTablePdf()
    {
      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToWithTablePdf);
      parser.SaveTextToFile(Constants.pathToEmptyDocText);
      Assert.AreEqual(Constants.withTableExpectedResult + Constants.textSharpPdfParserEnding, parser.Text);
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

      Parser parser = new TextSharpPdfParser();
      parser.Parse(Constants.pathToHugeDocumentPdf);
      parser.SaveTextToFile(Constants.pathToEmptyDocText);
      stopwatch.Stop();

      Assert.Less(stopwatch.Elapsed, TimeSpan.FromSeconds(20));
    }
    #endregion
  }
}
