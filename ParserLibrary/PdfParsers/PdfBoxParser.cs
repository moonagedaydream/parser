using System;
using System.IO;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;

namespace ParserLibrary.PdfParsers {
    public class PdfBoxParser : Parser {
        public PdfBoxParser() : base(ParserType.PdfParser) {
        }

        public override void Parse(string fileToParse) {

            ParsedFile = fileToParse;

            CheckFileType();

            try {
                PDDocument document = PDDocument.load(ParsedFile);

                PDFTextStripper stripper = new PDFTextStripper();

                Text = stripper.getText(document);
                Size = new FileInfo(fileToParse).Length;
                Status = ParsingStatus.Completed;

                document.close();
            } catch (Exception) {
                Status = ParsingStatus.Error;
                throw;
            }
        }
    }
}
