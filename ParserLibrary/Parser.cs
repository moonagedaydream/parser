using System;
using System.IO;
using System.Threading.Tasks;

namespace ParserLibrary {
    public enum ParsingStatus {
        NotStarted = 0,
        Completed = 1,
        Error = 2
    }

    public enum ParserType {
        PdfParser = 0,
        DocParser = 1,
        HtmlParser = 2
    }

    public abstract class Parser {
        //Path to a file
        public string ParsedFile { get; protected set; }

        //Parsed text
        public string Text { get; protected set; }

        //Type of the parser
        public ParserType Type { get; private set; }

        //Document size (bytes)
        public long Size { get; protected set; }

        //Status of parsing process
        public ParsingStatus Status { get; protected set; }

        protected Parser(ParserType type) {
            Type = type;
            Status = ParsingStatus.NotStarted;
        }

        //Prepares parser and parses the document
        public abstract void Parse(string fileToParse);

        //Saves parsed text to a file
        public void SaveTextToFile(string filePath) {
            if (Status != ParsingStatus.Completed) {
                throw new InvalidOperationException(
                    "Ths operation is avaliable only after parsing is successfully finished.");
            }
            File.WriteAllText(filePath, Text);
        }

        //Checks that the extension of the file is correct
        protected void CheckFileType() {
            if (!string.IsNullOrWhiteSpace(ParsedFile)) {
                switch (Type) {
                    case ParserType.PdfParser:
                        if (ParsedFile.EndsWith(".pdf")) {
                            return;
                        }
                        break;
                    case ParserType.DocParser:
                        if (ParsedFile.EndsWith(".doc") || ParsedFile.EndsWith(".docx")) {
                            return;
                        }
                        break;
                    case ParserType.HtmlParser:
                        if (ParsedFile.EndsWith(".html")) {
                            return;
                        }
                        break;
                }
            }
            throw new ArgumentException("Wrong type of file for this parser.");
        }
    }
}
