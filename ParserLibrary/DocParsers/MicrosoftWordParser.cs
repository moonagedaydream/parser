using System;
using System.IO;
using System.Text;
using Microsoft.Office.Interop.Word;

namespace ParserLibrary.DocParsers {
    public class MicrosoftWordParser : Parser {
        public MicrosoftWordParser()
            : base(ParserType.DocParser) {
        }

        public override void Parse(string fileToParse) {
            ParsedFile = fileToParse;

            CheckFileType();

            try {
                Application word = new Application();
                object miss = System.Reflection.Missing.Value;
                object path = fileToParse;
                object readOnly = true;
                Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
                StringBuilder text = new StringBuilder();
                for (int i = 0; i < docs.Paragraphs.Count; i++) {
                    text.AppendLine(docs.Paragraphs[i + 1].Range.Text);
                }
                docs.Close();
                word.Quit();

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
