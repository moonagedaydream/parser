using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ParserLibrary.HtmlParsers {
    public abstract class HtmlParser : Parser {

        public List<string> Links;

        public Encoding Encoding;

        protected HtmlParser()
            : base(ParserType.HtmlParser) {
            Links = new List<string>();
        }

        protected void TryAddLink(string link) {

            if (link == null) {
                return;
            }

            Uri uri;

            if (Uri.TryCreate(link, UriKind.RelativeOrAbsolute, out uri)) {

                string uriString = uri.ToString();

                if (uriString.Contains("://www.")) {
                    uriString = uriString.Replace("://www.", "://");
                }

                if (!uriString.EndsWith("/")) {
                    uriString += "/";
                }

                if (!Links.Contains(uriString)) {
                    Links.Add(uriString);
                }
            }
        }

        public List<Uri> GetLinks(string globalUri) {
            List<Uri> result = new List<Uri>();
            Uri uri;

            foreach (string link in Links) {

                string editedLink = link;

                if (editedLink.StartsWith("#")) {
                    continue;
                }

                if (!editedLink.StartsWith("http") && editedLink.Contains("http"))
                {
                    editedLink = editedLink.Substring(editedLink.IndexOf("http", System.StringComparison.Ordinal));
                }

                try {
                    editedLink = Uri.TryCreate(editedLink, UriKind.Relative, out uri) ?
                        new Uri(new Uri(globalUri), uri.ToString()).ToString() :
                        Uri.TryCreate(editedLink, UriKind.Absolute, out uri) ?
                            uri.ToString() : null;

                    if (!string.IsNullOrEmpty(editedLink)) {
                        editedLink = editedLink.Replace("/?", "?");

                        if (!editedLink.StartsWith("http")) {
                            continue;
                        }

                        result.Add(new Uri(editedLink));
                    }
                } catch (Exception) {
                }
            }
            return result.Distinct().ToList();
        } 

        protected void GetLinksUsingRegex(string urlMatch = null) {

            string text = File.ReadAllText(ParsedFile);

            Regex cHttpUrlsRegex = new Regex(@"(?<url>((http|https):[/][/]|www.)([a-z]|[A-Z]|[0-9]|[_/.=&?%-]|[~])*)", RegexOptions.IgnoreCase);

            if (String.IsNullOrEmpty(text)) {
                return;
            }

            MatchCollection matches = cHttpUrlsRegex.Matches(text);

            var vMatcher = urlMatch == null ? null : new Regex(urlMatch);

            foreach (Match match in matches) {

                string url = match.Groups["url"].Value;

                TryAddLink(url);
            }

        }

        protected void GetEncoding() {
            try {
                using (FileStream fs = File.OpenRead(ParsedFile)) {
                    Ude.CharsetDetector cdet = new Ude.CharsetDetector();
                    cdet.Feed(fs);
                    cdet.DataEnd();
                    if (cdet.Charset != null) {
                        Encoding = Encoding.GetEncoding(cdet.Charset);
                    }
                }
            } catch (Exception) {
                Encoding = Encoding.Unicode;
            }
            if (Encoding == null) {
                Encoding = Encoding.Unicode;
            }
        }

        public static Encoding GetEncoding(byte[] byteArray)
        {
          Encoding encoding = null;
          try
          {
            using (Stream stream = new MemoryStream(byteArray))
            {
              var cdet = new Ude.CharsetDetector();
              cdet.Feed(stream);
              cdet.DataEnd();
              if (cdet.Charset != null)
              {
                encoding = Encoding.GetEncoding(cdet.Charset);
              }
            }
          }
          catch (Exception)
          {
            encoding = Encoding.Unicode;
          }
          return encoding ?? (Encoding.Unicode);
        }

        public void SaveAllLinksToFile(string filePath) {
            if (Status != ParsingStatus.Completed) {
                throw new InvalidOperationException(
                    "Ths operation is avaliable only after parsing is successfully finished.");
            }
            File.WriteAllLines(filePath, Links);
        }

    }
}
