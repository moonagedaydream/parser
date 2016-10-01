using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AngleSharp.Dom.Html;
using HtmlAgilityPack;

namespace ParserLibrary.HtmlParsers {
    public class AngleSharpParser : HtmlParser {

        public AngleSharpParser() : base() { }

        public override void Parse(string fileToParse) {

            ParsedFile = fileToParse;

            CheckFileType();

            try {
                IHtmlDocument  document =
                    new AngleSharp.Parser.Html.HtmlParser().Parse(File.ReadAllText(ParsedFile));

                StringBuilder text = new StringBuilder();

                IEnumerable<string> paragraphs = document.QuerySelectorAll("p").Select(i => i.TextContent);

                foreach (string paragraph in paragraphs) {

                    text.AppendLine(paragraph);
                }

                Text = text.ToString();

                IEnumerable<string> hrefs = document.QuerySelectorAll("a").Select(i => i.GetAttribute("href"));

                foreach (string href in hrefs) {
                    TryAddLink(href);
                }

                GetLinksUsingRegex();

                Size = new FileInfo(fileToParse).Length;
                Status = ParsingStatus.Completed;

            } catch (Exception) {
                Status = ParsingStatus.Error;
                throw;
            }
        }
    }
}
