﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace ParserLibrary.HtmlParsers {
    public class HtmlAgilityPackParser : HtmlParser {

        public HtmlAgilityPackParser() : base() { }

        public override void Parse(string fileToParse) {

            ParsedFile = fileToParse;

            CheckFileType();

            try {
                HtmlWeb htmlWeb = new HtmlWeb();

                htmlWeb.OverrideEncoding = GetEncoding();

                HtmlDocument document = htmlWeb.Load(ParsedFile);

                StringBuilder text = new StringBuilder();

                IEnumerable<string> paragraphs = document.DocumentNode.Descendants("p")
                    .Select(a => a.InnerText);

                foreach (string paragraph in paragraphs) {

                    text.AppendLine(paragraph);
                }

                Text = text.ToString();

                IEnumerable<string> hrefs = document.DocumentNode.Descendants("a")
                    .Select(a => a.GetAttributeValue("href", null));

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