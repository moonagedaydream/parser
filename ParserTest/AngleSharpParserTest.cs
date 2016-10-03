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
      File.Delete(Constants.pathToEmptyDocText2);
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToAppliedMathematicMainPageHtml);
      parser.SaveTextToFile(Constants.pathToEmptyDocText);
      parser.SaveAllLinksToFile(Constants.pathToEmptyDocText2);
      Assert.IsTrue(File.Exists(Constants.pathToEmptyDocText), "TXT file with parsed text was not created.");
      Assert.IsTrue(File.Exists(Constants.pathToEmptyDocText2), "TXT file with links was not created.");

    }

    /// <summary>
    /// Parse html page with Chinese symbols.
    /// </summary>
    [Test]
    public void ChineseSymbolsHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToChineseSymbolsHtml);
      
      Assert.AreEqual(Constants.emptyDocExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse html with columns and numeration.
    /// </summary>
    [Test]
    public void ColumnsAndNumerationHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToColumnAndNumerationHtml);
      parser.SaveAllLinksToFile(Constants.pathToEmptyDocText2);
      //Assert.AreEqual(Constants.columnAndNumerationExpectedResult, parser.Text);
      CollectionAssert.AreEqual(new List<string>(), parser.Links);
    }

    /// <summary>
    /// Parse html with contents.
    /// </summary>
    [Test]
    public void ContentsHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToContentsHtml);

      Assert.AreEqual(Constants.contentsExpectedResult, parser.Text);
    }

    /// <summary>
    /// Parse html with no text.
    /// </summary>
    [Test]
    public void NoTextAndLinksHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToEmptyTextAndLinksHtml);

      Assert.AreEqual(String.Empty, parser.Text);
      CollectionAssert.AreEqual(new List<string>(), parser.Links);
    }

    /// <summary>
    /// Parse html with English letters.
    /// </summary>
    [Test]
    public void EnglishLettersHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToEnglishLettersHtml);

      Assert.AreEqual(Constants.EnglishLettersExpectedResult + Constants.htmlAngleSharpParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse html with English letters.
    /// </summary>
    [Test]
    public void EnglishTextHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToEnglishTextHtml);

      Assert.AreEqual(Constants.EnglishTextExpectedResult + Constants.htmlAngleSharpParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse html with false encoding in body.
    /// </summary>
    [Test]
    public void FalseEncodingInBodyHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToFalseEncodingInBodyHtml);

      Assert.AreEqual(Constants.falseEncodingInBodyExpectedResult + Constants.htmlAngleSharpParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse html with local links.
    /// </summary>
    [Test]
    public void LocalLinksHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToLocalLinksHtml);
      parser.SaveTextToFile(Constants.pathToEmptyDocText);
      CollectionAssert.AreEqual(new List<string>(), parser.Links);
    }

    /// <summary>
    /// Parse huge html.
    /// </summary>
    [Test]
    public void HugeHtml()
    {
      File.Delete(Constants.pathToEmptyDocText);
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToHugeDocumentHtml);
      parser.SaveTextToFile(Constants.pathToEmptyDocText);
      stopwatch.Stop();

      Assert.Less(stopwatch.Elapsed, TimeSpan.FromSeconds(2));
    }
    #endregion
  }
}
