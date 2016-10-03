﻿using System;
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
    public static string pathToEmptyPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "Empty.pdf");
    public static string emptyDocExpectedResult = string.Empty;
    
    public static string pathToEmptyDocText = Path.Combine(Environment.CurrentDirectory, @"ResultDocuments\", "Empty.txt");
    public static string pathToEmpty0SizeDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "Empty0Size.docx");
    public static string pathToEmpty0SizePdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "Empty0Size.pdf");

    public static string pathToDocDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "Doc.doc");
    public static string docDocExpectedResult = "Test";
    
    public static string pathToEnglishLettersDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "EnglishLetters.docx");
    public static string pathToEnglishLettersPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "EnglishLetters.pdf");
    public static string EnglishLettersExpectedResult = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz";
    
    public static string pathToHugeDocumentDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "HugeDocument.docx");
    public static string pathToHugeDocumentPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "HugeDocument.pdf");
    
    public static string pathToNumbersDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "Numbers.docx");
    public static string pathToNumbersPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "Numbers.pdf");
    public static string numbersExpectedResult = "0123456789";
    
    public static string pathToRussianLettersDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "RussianLetters.docx");
    public static string pathToRussianLettersPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "RussianLetters.pdf");
    public static string RussianLettersExpectedResult = "ЁАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдежзийклмнопрстуфхцчшщъыьэюяё";
    
    public static string pathToSpecialCharactersDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "SpecialCharacters.docx");
    public static string pathToSpecialCharactersPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "SpecialCharacters.pdf");
    public static string specialCharactersExpectedResult = GetSpecialCharactersString();

    public static string specialCharactersExpectedResultPdf =
      " !\"#$%&'()*+,-\r\n./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}\r\n~ ¡¢£¤¥¦§¨©ª«¬-®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìí\r\nîïðñòóôõö÷øùúûüýþÿŒœŠšŸŽžƒˆ˜–—‘’‚“”„†‡•…‰‹›€™\r\n";

    public static string specialCharactersExpectedResultPdf2 =
      " !\"#$%&'()*+,-\n./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}\n~ ¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìí\nîïðñòóôõö÷øùúûüýþÿŒœŠšŸŽžƒˆ˜–—‘’‚“”„†‡•…‰‹›€™";   
    public static string pathToWithColumnsDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithColumns.docx");
    public static string pathToWithColumnsPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "WithColumns.pdf");
    public static string withColumnsExpectedResult = "This is the film version of the Pulitzer and Tony Award winning musical about Bohemians in the East Village of New York City struggling with life, love and AIDS, and the impacts they have on America.";
    public static string withColumnsExpectedResultPdf = "This is \r\nthe \r\nfilm \r\nversion \r\nof the \r\nPulitze\r\nr and \r\nTony \r\nAward \r\nwinnin\r\ng \r\nmusica\r\nl about \r\nBohem\r\nians in \r\nthe \r\nEast \r\nVillage \r\nof New \r\nYork \r\nCity \r\nstruggl\r\ning \r\nwith \r\nlife, \r\nlove \r\nand \r\nAIDS, \r\nand \r\nthe \r\nimpact\r\ns they \r\nhave \r\non \r\nAmeric\r\na.";

    public static string pathToWithContentsDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithContents.docx");
    public static string pathToWithContentsPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "WithContents.pdf");
    public static string withContentsExpectedResult1 = "ОглавлениеВведите название главы (уровень 1)1Введите название главы (уровень 2)2Введите название главы (уровень 3)3Введите название главы (уровень 1)4Введите название главы (уровень 2)5Введите название главы (уровень 3)6";
    public static string withContentsExpectedResult2 = "Оглавление\r\r\nВведите название главы (уровень 1)01\r\r\nВведите название главы (уровень 2)02\r\r\nВведите название главы (уровень 3)03\r\r\nВведите название главы (уровень 1)04\r\r\nВведите название главы (уровень 2)05\r\r\nВведите название главы (уровень 3)06\r\r\n";
    public static string withContentsExpectedResultPdf = "Оглавление \nВведите название главы (уровень 1) ....................................................................................................... 1 \nВведите название главы (уровень 2) ..................................................................................................... 2 \nВведите название главы (уровень 3) ................................................................................................. 3 \nВведите название главы (уровень 1) ....................................................................................................... 4 \nВведите название главы (уровень 2) ..................................................................................................... 5 \nВведите название главы (уровень 3) ................................................................................................. 6 \n";
    public static string withContentsExpectedResultPdf2 = "Оглавление \r\nВведите название главы (уровень 1) ....................................................................................................... 1 \r\nВведите название главы (уровень 2) ..................................................................................................... 2 \r\nВведите название главы (уровень 3) ................................................................................................. 3 \r\nВведите название главы (уровень 1) ....................................................................................................... 4 \r\nВведите название главы (уровень 2) ..................................................................................................... 5 \r\nВведите название главы (уровень 3) ................................................................................................. 6 \r\n";

    public static string pathToWithFootnotesDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithFootnotes.docx");
    public static string pathToWithFootnotesPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "WithFootnotes.pdf");
    public static string withFootnotesResult = "One of the most iconic characters in popular culture,[3][4]";
    public static string withFootnotesResult2 = "One of the [endnoteRef:2]most iconic characters in popular [footnoteRef:2]culture,[3][4]";
    public static string withFootnotesResult3 = "One of the \u0002most iconic characters in popular \u0002culture,[3][4]";

    public static string withFootnotesResultPdf =
      "One of the \r\ni\r\nmost iconic characters in popular \r\n1\r\nculture,[3][4] \r\n                                                          \r\ni\r\n New \r\n                                                          \r\n1\r\n test";

    public static string pathToWithFormulasDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithFormulas.docx");
    public static string pathToWithFormulasPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "WithFormulas.pdf");
    public static string withFormulasExpectedResult1 = "x+an=k=0nnkxkan-k";
    
    public static string pathToWithNumerationDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithNumeration.docx");
    public static string pathToWithNumerationPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "WithNumeration.pdf");
    public static string withNumerationExpectedResult1 = "Extreme JusticeJustice League 3000Justice League AntarcticaJustice League DarkJustice League EliteJustice League EuropeJustice League InternationalJustice League Task ForceJustice League UnitedJustice LeaguesSuper BuddiesSuper FriendsShadowpactYoung Justice";
    public static string withNumerationExpectedResult2 = "Extreme Justice\r\r\nJustice League 3000\r\r\nJustice League Antarctica\r\r\nJustice League Dark\r\r\nJustice League Elite\r\r\nJustice League Europe\r\r\nJustice League International\r\r\nJustice League Task Force\r\r\nJustice League United\r\r\nJustice Leagues\r\r\nSuper Buddies\r\r\nSuper Friends\r\r\nShadowpact\r\r\nYoung Justice";
    public static string withNumerationExpectedResultPdf = " Extreme Justice \n Justice League 3000 \n Justice League Antarctica \n1) Justice League Dark \ni) Justice League Elite \n1) Justice League Europe \n2) Justice League International \na) Justice League Task Force \n Justice League United \nII. Justice Leagues \n1. Super Buddies \n Super Friends \n2. Shadowpact \n3. Young Justice";
    public static string withNumerationExpectedResultPdf2 = " Extreme Justice \r\n Justice League 3000 \r\n Justice League Antarctica \r\n1) Justice League Dark \r\ni) Justice League Elite \r\n1) Justice League Europe \r\n2) Justice League International \r\na) Justice League Task Force \r\n Justice League United \r\nII. Justice Leagues \r\n1. Super Buddies \r\n Super Friends \r\n2. Shadowpact \r\n3. Young Justice";

    public static string pathToWithPictureDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithPicture.docx");
    public static string pathToWithPicturePdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "WithPicture.pdf");
    public static string withPictureExpectedResult = " Dragon Age is a high fantasy role-playing video game series created by BioWare. The first game, Dragon Age: Origins, was released in 2009. Dragon Age II, a sequel to Origins, was released in March 2011.";
    public static string withPictureExpectedResultPdf = " Dragon Age is a high fantasy role-playing video game series created by \nBioWare. The first \ngame, Dragon Age: \nOrigins, was released in \n2009. Dragon Age II, a \nsequel to Origins, was \nreleased in March 2011. \n";
    public static string withPictureExpectedResultPdf2 = " Dragon Age is a high fantasy role-playing video game series created by \r\nBioWare. The first \r\ngame, Dragon Age: \r\nOrigins, was released in \r\n2009. Dragon Age II, a \r\nsequel to Origins, was \r\nreleased in March 2011. \r\n";

    public static string pathToWithTableDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithTable.docx");
    public static string pathToWithTablePdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "WithTable.pdf");
    public static string withTableExpectedResult = "Based on Bioware's history, a \"Dragon Age\" game usually drops after the last DLC of the \"Mass Effect\" game before it. The only exception is when Bioware released \"Dragon Age: Origins - Awakening\" before \"Mass Effect 2\" finished its run.Eurogamer has also noted that with \"Mass Effect: Andromeda\" entering its final production stages, and with both Laidlaw and Weekes not working on Bioware's secret new IP, there is a huge possibility that Kennedy was taken onboard for a \"Dragon Age\" related project.";
    public static string withTableExpectedResult2 = "Based on Bioware's history, a \"Dragon Age\" \r\a\r\ngame usually drops after the last DLC of the \"Mass Effect\" \r\a\r\n\r\a\r\ngame before it. \r\a\r\nThe only exception is when Bioware released \"Dragon Age: Origins - Awakening\" before \"Mass Effect 2\" finished its run.\r\a\r\n\r\a\r\nEurogamer has also noted that with \"Mass Effect: Andromeda\" entering its final production stages, and with both Laidlaw and Weekes not working on Bioware's secret new IP, there is a huge possibility that Kennedy was taken onboard for a \"Dragon Age\" related project.";
    public static string withTableExpectedResultPdf = "Eurogamer has also noted that with \"Mass Effect: Andromeda\" entering its final \r\nproduction stages, and with both Laidlaw and Weekes not working on Bioware's \r\nsecret new IP, there is a huge possibility that Kennedy was taken onboard for a \r\n\"Dragon Age\" related project. \r\nBased on Bioware's history, a \"Dragon \r\nAge\"  \r\ngame usually drops after the last DLC of \r\nthe \"Mass Effect\"  \r\ngame before it.  The only exception is when Bioware \r\nreleased \"Dragon Age: Origins - \r\nAwakening\" before \"Mass Effect 2\" \r\nfinished its run.";

    public static string pathToWithLinkDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "WithLink.docx");
    public static string pathToWithLinkPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "WithLink.pdf");
    public static string withLinkExpectedResult = "The Joker is a fictional supervillain created by Bill Finger, Bob Kane,";
    public static string withLinkExpectedResulPdf = "The Joker is a fictional supervillain created \nby Bill Finger, Bob Kane,";
    public static string withLinkExpectedResulPdf2 = "The Joker is a fictional supervillain created \r\nby Bill Finger, Bob Kane,";


    public static string microsoftWordParserEnding = "\r\r\n";
    public static string pdfBoxParserEnding = " \r\n";
    public static string textSharpPdfParserEnding = " ";
  }
}