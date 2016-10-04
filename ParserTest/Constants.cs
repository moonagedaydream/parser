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
    public static string pathToEmptyPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "Empty.pdf");
    public static string emptyDocExpectedResult = string.Empty;
    public static string chineseSymbolsExpectedResult = "\r\n九月21日，英国《泰晤士高等教育》全球大学年度排行榜公布的2...";

    public static string pathToChineseSymbolsHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "ChinishSymbols.html");

    public static string pathToEmptyDocText = Path.Combine(Environment.CurrentDirectory, @"ResultDocuments\", "Empty.txt");
    public static string pathToEmpty0SizeDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "Empty0Size.docx");
    public static string pathToEmpty0SizePdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "Empty0Size.pdf");
    public static string pathToEmptyDocText2 = Path.Combine(Environment.CurrentDirectory, @"ResultDocuments\", "Empty2.txt");

    public static string pathToAppliedMathematicMainPageHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "AppliedMathematicMainPage.html");

    public static string pathToDocDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "Doc.doc");
    public static string docDocExpectedResult = "Test";
    
    public static string pathToEnglishLettersDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "EnglishLetters.docx");
    public static string pathToEnglishLettersPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "EnglishLetters.pdf");
    public static string EnglishLettersExpectedResult = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz";
    public static string EnglishTextExpectedResult = "Faculty of applied mathematics and control processes was established\nOctober 10, 1969, on the base of departments of the Mathematics and\nMechanics Faculty and Research Institute.\r\nFaculty of Applied Mathematics and Control Processes of Saint-Petersburg State University takes one leading positions among other faculties of this kind in universities of  Russian Federation. Graduates of the faculty are in-demand among employers not only in Russia, but also abroad.\r\nA large variety of professions.\r\nStudents' trade union organization helps with employment; organizes events (seminars, presentations, job fair) for employers; finds backers for events of the faculty  \r\nGraduates of the faculty usually take leading positions. They work in IT-companies, industry, develop business, pursue science, teach and  make serious scientific research.\r\nNews archive >>";

    public static string EnglishTextExpectedResult2 =
      "Faculty of applied mathematics and control processes was established\nOctober 10, 1969, on the base of departments of the Mathematics and\nMechanics Faculty and Research Institute.\r\nFaculty of Applied Mathematics and Control Processes of Saint-Petersburg State University takes one leading positions among other faculties of this kind in universities of  Russian Federation. Graduates of the faculty are in-demand among employers not only in Russia, but also abroad.\r\nA large variety of professions.\r\nStudents&apos; trade union organization helps with employment; organizes events (seminars, presentations, job fair) for employers; finds backers for events of the faculty  \r\nGraduates of the faculty usually take leading positions. They work in IT-companies, industry, develop business, pursue science, teach and  make serious scientific research.\r\nNews archive&nbsp;&gt;&gt;";
    public static string falseEncodingInBodyExpectedResult = "23.09.2010\r\nВлад Мержевич";

    public static string localLinksExpectedResult = "23.09.2010\r\nВлад Мержевич";

    public static string pathToColumnAndNumerationHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "ColumnAndNumeration.html");
    public static string unknownExpectedResult = "_";
    public static string columnAndNumerationExpectedResult = "References ^ \"Captain America: Civil War (12A)\". British Board of Film Classification. April 18, 2016. Archived from the original on May 15, 2016. Retrieved April 18, 2016. ^ McMillian, Graeme (March 9, 2016). \"\'Captain America: Civil War\' Will Be the Longest Marvel Studios Film to Date\". The Hollywood Reporter. Archived from the original on March 9, 2016. Retrieved March 9, 2016.^ a b c \"Captain America: Civil War (2016)\". Box Office Mojo. Retrieved September 4, 2016.";

    public static List<string> columnAndNumerationLinks = new List<string>
    {
      "http://bbfc.co.uk/releases/captain-america-civil-war-film-0/",
      "http://webcitation.org/6hWife8Ar/",
      "http://hollywoodreporter.com/heat-vision/captain-america-civil-war-run-873534/",
      "http://webcitation.org/6ftL3F9vN/"
    }; 

    public static string pathToContentsHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "Contents.html");
    public static string contentsExpectedResult = "Contents 1 Plot 2 Cast 3 Production 3.1 Development 3.2 Pre-production 3.3 Filming 3.4 Post-production";

    public static string pathToEmptyTextAndLinksHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "EmptyTextAndLinks.html");
    public static string pathToEnglishLettersHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "EnglishLetters.html");
    public static string pathToEnglishTextHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "EnglishText.html");

    public static string pathToFalseEncodingInBodyHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "FalseEncodingInBody.html");
    public static string pathToLocalLinksHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "LocalLinks.html");
    public static string pathToNumbersHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "Numbers.html");
    public static string pathToCommentedLinksHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "CommentedLinks.html");

    public static string pathToRussianLettersHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "RussianLetters.html");
    public static string pathToSpecialCharactersHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "SpecialCharacters.html");

    public static string pathToWindows1251EncodingHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "Windows1251Encoding.html");


    public static List<string> localLinks = new List<string>
    {
      "/",
      "/AGSM/",
      "/BIOL/",
      "/Events/PhysTraining/"
    };


    public static string pathToAppliedMathematicMainPageyHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "AppliedMathematicMainPage.html");

    public static string pathToHugeDocumentDoc = Path.Combine(Environment.CurrentDirectory, @"DocTest\", "HugeDocument.docx");
    public static string pathToHugeDocumentPdf = Path.Combine(Environment.CurrentDirectory, @"PdfTest\", "HugeDocument.pdf");
    public static string pathToHugeDocumentHtml = Path.Combine(Environment.CurrentDirectory, @"HtmlTest\", "HugeDocument.html");

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
    public static string htmlAngleSharpParserEnding = "\r\n";
  }
}
