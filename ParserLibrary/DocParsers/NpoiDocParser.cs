using System;
using System.IO;
using System.Text;
using NPOI.XWPF.UserModel;

namespace ParserLibrary.DocParsers {
    public class NpoiDocParser : Parser {
        public NpoiDocParser()
            : base(ParserType.DocParser) {
        }

        public override void Parse(string fileToParse) {

            ParsedFile = fileToParse;

            CheckFileType();

            try {

                StringBuilder text = new StringBuilder();

                if (new FileInfo(ParsedFile).Length != 0) {

                    using (FileStream stream = File.OpenRead(fileToParse)) {
                        XWPFDocument document = new XWPFDocument(stream);
                        foreach (XWPFParagraph paragraph in document.Paragraphs) {
                            text.Append(paragraph.ParagraphText);
                        }
                    }
                }

                Text = text.ToString();
                Size = new FileInfo(fileToParse).Length;
                Status = ParsingStatus.Completed;
            } catch (Exception) {
                Status = ParsingStatus.Error;
                throw;
            }
        }
    }
}
