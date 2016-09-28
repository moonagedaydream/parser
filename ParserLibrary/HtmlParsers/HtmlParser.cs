using System;
using System.IO;

namespace ParserLibrary.HtmlParsers {
    public class HtmlParser : Parser {
        public HtmlParser()
            : base(ParserType.HtmlParser) {
        }

        public override void Parse(string fileToParse) {

            throw new NotImplementedException();

            ParsedFile = fileToParse;

            CheckFileType();

            try {

                

                //Text = ;
                Size = new FileInfo(fileToParse).Length;
                Status = ParsingStatus.Completed;

            } catch (Exception) {
                Status = ParsingStatus.Error;
                throw;
            }
        }
    }
}
