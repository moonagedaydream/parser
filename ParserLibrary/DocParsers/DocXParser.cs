﻿using System;
using System.IO;
using System.Text;
using Novacode;

namespace ParserLibrary.DocParsers {
    public class DocXParser : Parser {
        public DocXParser()
            : base(ParserType.DocParser) {
        }

        public override void Parse(string fileToParse) {

            ParsedFile = fileToParse;

            CheckFileType();

            try {

                StringBuilder text = new StringBuilder();

                if (new FileInfo(ParsedFile).Length != 0) {

                    DocX document = DocX.Load(ParsedFile);

                    foreach (Paragraph paragraph in document.Paragraphs) {
                        text.Append(paragraph.Text);
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
