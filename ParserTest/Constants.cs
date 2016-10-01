using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ParserTest
{
  class Constants
  {
    #region Private methods

    private static string GetSpecialCharactersString()
    {
      List<Char> printableChars = new List<char>();
      for (int i = 32; i <= 8482; i++)
      {
        char c = Convert.ToChar(i);
        if (!char.IsControl(c))
        {
          printableChars.Add(c);
        }
      }
      var sb = new StringBuilder();
      foreach (var c in printableChars)
      {
        sb.Append(c);
      }
      return sb.ToString();
    }

    #endregion

    public static string pathToEmptyDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "Empty.docx");
    public static string emptyDocExpectedResult = string.Empty;
    
    public static string pathToEmptyDocText = Path.Combine(Environment.CurrentDirectory, @"ResultDocuments\", "Empty.txt");
    public static string pathToEmpty0SizeDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "Empty0Size.docx");
    
    public static string pathToDocDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "Doc.doc");
    public static string docDocExpectedResult = "Test";
    
    public static string pathToEnglishLettersDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "EnglishLetters.docx");
    public static string EnglishLettersExpectedResult = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz";
    
    public static string pathToHugeDocumentDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "HugeDocument.docx");
    
    
    public static string pathToNumbersDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "Numbers.docx");
    public static string numbersExpectedResult = "0123456789";
    
    public static string pathToRussianLettersDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "RussianLetters.docx");
    public static string RussianLettersExpectedResult = "ЁАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдежзийклмнопрстуфхцчшщъыьэюяё";
    
    public static string pathToSpecialCharactersDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "SpecialCharacters.docx");
    public static string specialCharactersExpectedResult = GetSpecialCharactersString();
    
    public static string pathToWithColumnsDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithColumns.docx");
    public static string withColumnsExpectedResult = "This is the film version of the Pulitzer and Tony Award winning musical about Bohemians in the East Village of New York City struggling with life, love and AIDS, and the impacts they have on America.";

    public static string pathToWithContentsDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithContents.docx");
    public static string withContentsExpectedResult1 = "ОглавлениеВведите название главы (уровень 1)1Введите название главы (уровень 2)2Введите название главы (уровень 3)3Введите название главы (уровень 1)4Введите название главы (уровень 2)5Введите название главы (уровень 3)6";
    public static string withContentsExpectedResult2 = "Оглавление\r\r\nВведите название главы (уровень 1)01\r\r\nВведите название главы (уровень 2)02\r\r\nВведите название главы (уровень 3)03\r\r\nВведите название главы (уровень 1)04\r\r\nВведите название главы (уровень 2)05\r\r\nВведите название главы (уровень 3)06\r\r\n";

    public static string pathToWithFootnotesDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithFootnotes.docx");
    public static string withFootnotesResult = "One of the most iconic characters in popular culture,[3][4]";
    public static string withFootnotesResult2 = "One of the [endnoteRef:2]most iconic characters in popular [footnoteRef:2]culture,[3][4]";
    public static string withFootnotesResult3 = "One of the \u0002most iconic characters in popular \u0002culture,[3][4]";

    public static string pathToWithFormulasDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithFormulas.docx");
    public static string withFormulasExpectedResult1 = "x+an=k=0nnkxkan-k";
    
    public static string pathToWithNumerationDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithNumeration.docx");
    public static string withNumerationExpectedResult1 = "Extreme JusticeJustice League 3000Justice League AntarcticaJustice League DarkJustice League EliteJustice League EuropeJustice League InternationalJustice League Task ForceJustice League UnitedJustice LeaguesSuper BuddiesSuper FriendsShadowpactYoung Justice";
    public static string withNumerationExpectedResult2 = "Extreme Justice\r\r\nJustice League 3000\r\r\nJustice League Antarctica\r\r\nJustice League Dark\r\r\nJustice League Elite\r\r\nJustice League Europe\r\r\nJustice League International\r\r\nJustice League Task Force\r\r\nJustice League United\r\r\nJustice Leagues\r\r\nSuper Buddies\r\r\nSuper Friends\r\r\nShadowpact\r\r\nYoung Justice";

    public static string pathToWithPictureDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithPicture.docx");
    public static string withPictureExpectedResult = " Dragon Age is a high fantasy role-playing video game series created by BioWare. The first game, Dragon Age: Origins, was released in 2009. Dragon Age II, a sequel to Origins, was released in March 2011.";
    
    public static string pathToWithTableDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithTable.docx");
    public static string withTableExpectedResult = "Based on Bioware's history, a \"Dragon Age\" game usually drops after the last DLC of the \"Mass Effect\" game before it. The only exception is when Bioware released \"Dragon Age: Origins - Awakening\" before \"Mass Effect 2\" finished its run.Eurogamer has also noted that with \"Mass Effect: Andromeda\" entering its final production stages, and with both Laidlaw and Weekes not working on Bioware's secret new IP, there is a huge possibility that Kennedy was taken onboard for a \"Dragon Age\" related project.";
    public static string withTableExpectedResult2 = "Based on Bioware's history, a \"Dragon Age\" \r\a\r\ngame usually drops after the last DLC of the \"Mass Effect\" \r\a\r\n\r\a\r\ngame before it. \r\a\r\nThe only exception is when Bioware released \"Dragon Age: Origins - Awakening\" before \"Mass Effect 2\" finished its run.\r\a\r\n\r\a\r\nEurogamer has also noted that with \"Mass Effect: Andromeda\" entering its final production stages, and with both Laidlaw and Weekes not working on Bioware's secret new IP, there is a huge possibility that Kennedy was taken onboard for a \"Dragon Age\" related project.";

    public static string pathToWithLinkDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithLink.docx");
    public static string withLinkExpectedResult = "The Joker is a fictional supervillain created by Bill Finger, Bob Kane,";

    public static string microsoftWordParserEnding = "\r\r\n";
  }
}
