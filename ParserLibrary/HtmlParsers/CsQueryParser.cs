using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsQuery;
using HtmlAgilityPack;
using org.bouncycastle.mail.smime.handlers;
using sun.reflect.generics.tree;

namespace ParserLibrary.HtmlParsers {
    public class CsQueryParser : HtmlParser {

        public CsQueryParser() : base() { }

        public override void Parse(string fileToParse) {

            ParsedFile = fileToParse;

            CheckFileType();

            try {

                StringBuilder text = new StringBuilder();

                using (Stream stream = File.Open(ParsedFile,FileMode.Open)) {

                    CQ csQueryDocument = CQ.Create(stream);

                    //File.ReadAllText(ParsedFile);//CQ.Create(fileToParse);

                    CQ paragraphs = csQueryDocument["p"];

                    //IEnumerable<string> paragraphs = csQueryDocument["p"].Select(i => i.)

                    foreach (IDomObject paragraph in paragraphs) {

                        text.AppendLine(paragraph.InnerText);
                    }

                    Text = text.ToString();

                   /* byte[] bytes = GetEncoding().GetBytes(Text);

                    // Perform the conversion from one encoding to the other.
                    byte[] asciiBytes = Encoding.Convert(GetEncoding(), Encoding.Unicode, bytes);

                    // Convert the new byte[] into a char[] and then into a string.
                    char[] asciiChars = new char[Encoding.Unicode.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                    Encoding.Unicode.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
                    string asciiString = new string(asciiChars);

                    Text = asciiString;*/

                    CQ hrefs = csQueryDocument["a"];

                    foreach (IDomObject href in hrefs) {
                        TryAddLink(href.GetAttribute("href"));
                    }

                    GetLinksUsingRegex();

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
