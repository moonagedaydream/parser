using System;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace ParserLibrary.PdfParsers {
    public class TextSharpPdfParser : Parser {

        public TextSharpPdfParser() : base(ParserType.PdfParser) { }

        public override void Parse(string fileToParse) {

            ParsedFile = fileToParse;

            CheckFileType();

            try {

                using (PdfReader reader = new PdfReader(fileToParse)) {

                    StringBuilder text = new StringBuilder();

                    for (int i = 1; i <= reader.NumberOfPages; i++) {
                        text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                    }

                    Text = text.ToString();
                    Size = new FileInfo(fileToParse).Length;
                    Status = ParsingStatus.Completed;
                }
            } catch (Exception) {
                Status = ParsingStatus.Error;
                throw;
            }

        }

    }
}
