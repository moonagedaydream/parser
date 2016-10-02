using System;
using System.IO;
using System.Text;
using CsQuery;

namespace ParserLibrary.HtmlParsers {
    public class CsQueryParser : HtmlParser {
        public CsQueryParser() : base() {}

        public override void Parse(string fileToParse) {
            ParsedFile = fileToParse;

            CheckFileType();

            try {
                StringBuilder text = new StringBuilder();

                string s = File.ReadAllText(ParsedFile);

                CQ csQueryDocument = CQ.Create(s);

                CQ paragraphs = csQueryDocument["p"];

                foreach (IDomObject paragraph in paragraphs) {
                    text.AppendLine(paragraph.Cq().Text());
                }

                Text = text.ToString();

                CQ hrefs = csQueryDocument["a"];

                foreach (IDomObject href in hrefs) {
                    TryAddLink(href.GetAttribute("href"));
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
