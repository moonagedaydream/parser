using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using NUnit.Framework;
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
      
      Assert.AreEqual(Constants.chineseSymbolsExpectedResult + Constants.htmlAngleSharpParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse html with columns and numeration.
    /// </summary>
    [Test]
    public void OuterLinksHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToColumnAndNumerationHtml);

      CollectionAssert.AreEqual(Constants.columnAndNumerationLinks, parser.Links);
    }

    /// <summary>
    /// Parse html with no text.
    /// </summary>
    [Test]
    public void NoTextAndLinksHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToEmptyTextAndLinksHtml);

      CollectionAssert.AreEqual(new List<string>(), parser.Links);
    }

    /// <summary>
    /// Parse html with no text.
    /// </summary>
    [Test]
    public void NoTextAndLinksTextHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToEmptyTextAndLinksHtml);

      Assert.AreEqual(String.Empty, parser.Text);
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
    public void DetectEncodingMetaCharsetHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToFalseEncodingInBodyHtml);

      Assert.AreEqual(Encoding.UTF8, parser.Encoding);
    }

    /// <summary>
    /// Parse html with false encoding in body.
    /// </summary>
    [Test]
    public void DetectEncodingXmlEncodingHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToAppliedMathematicMainPageyHtml);

      Assert.AreEqual(Encoding.UTF8, parser.Encoding);    
    }

    /// <summary>
    /// Parse html with links.
    /// </summary>
    [Test]
    public void LocalLinksHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToLocalLinksHtml);

      CollectionAssert.AreEqual(Constants.localLinks, parser.Links);
    }

    /// <summary>
    /// Parse html with local links.
    /// </summary>
    [Test]
    public void NumbersTextHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToNumbersHtml);

      Assert.AreEqual(Constants.numbersExpectedResult+Constants.htmlAngleSharpParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse html with only commented links.
    /// </summary>
    [Test]
    public void CommentedLinksHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToCommentedLinksHtml);

      CollectionAssert.AreEqual(new List<string>(), parser.Links);
    }

    /// <summary>
    /// Parse html with Russian letters.
    /// </summary>
    [Test]
    public void RussianLettersTextHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToRussianLettersHtml);

      Assert.AreEqual(Constants.RussianLettersExpectedResult + Constants.htmlAngleSharpParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse html with special characters.
    /// </summary>
    [Test]
    [Ignore("Ignore special characters")]
    public void SpecialCharactersTextHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToSpecialCharactersHtml);
      
      Assert.AreEqual(Constants.specialCharactersExpectedResult + Constants.htmlAngleSharpParserEnding, parser.Text);
    }

    /// <summary>
    /// Parse html with special characters.
    /// </summary>
    [Test]
    public void Windows1251EncodingHtml()
    {
      HtmlParser parser = new AngleSharpParser();
      parser.Parse(Constants.pathToWindows1251EncodingHtml);

      Assert.AreEqual(Encoding.GetEncoding("windows-1251"), parser.Encoding);
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
