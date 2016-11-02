using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using log4net;
using ParserLibrary.HtmlParsers;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper
{
  /// <summary>
  /// CLass for providing methods for single html page download.
  /// </summary>
  internal sealed class SinglePageDownload
  {
    #region Private fields.
    /// <summary>
    /// Logger.
    /// </summary>
    private static readonly ILog log = LogManager.GetLogger("WebCrawler");
    #endregion

    #region Private methods.
    /// <summary>
    /// Donwload binary.
    /// </summary>
    private static byte[] DownloadBinary(Uri absoluteUri)
    {
      log.InfoFormat("Reading content from URL '{0}'.",
        absoluteUri);

      byte[] binaryContent;

      try
      {
        var request = (HttpWebRequest)WebRequest.Create(absoluteUri);

        var cachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
        request.CachePolicy = cachePolicy;

        using (var response = (HttpWebResponse)request.GetResponse())
        using (var stream = response.GetResponseStream())
        using (var memoryStream = new MemoryStream())
        {
          const int blockSize = 16384;
          var blockBuffer = new byte[blockSize];
          int read;

          while (stream != null && (read = stream.Read(blockBuffer, 0, blockSize)) > 0)
          {
            memoryStream.Write(blockBuffer, 0, read);
          }

          memoryStream.Seek(0, SeekOrigin.Begin);

          binaryContent = memoryStream.GetBuffer();
        }
      }
      catch (WebException webException)
      {
        if (webException.Status == WebExceptionStatus.ProtocolError)
        {
          var response = (HttpWebResponse)webException.Response;

          DatabaseSaver.UpdateNonWorkingUrl(absoluteUri, (int)response.StatusCode);

          if (response.StatusCode == HttpStatusCode.NotFound ||
            response.StatusCode == HttpStatusCode.InternalServerError)
          {
            log.Error(webException);

            binaryContent = null;
          }
          else
          {
            throw;
          }
        }
        else
        {
          throw;
        }
      }
      return binaryContent;
    }
    #endregion

    #region Public methods.
    /// <summary>
    /// Download a HTML page. 
    /// </summary>
    public static string DownloadHtml(
      Uri absoluteUri,
      out Encoding encoding,
      CrawlerOptions options)
    {
      byte[] binaryContent = DownloadBinary(absoluteUri);

      encoding = HtmlParser.GetEncoding(binaryContent);

      log.InfoFormat("Detected encoding '{0}' for HTML document from URL '{1}'.",
        encoding.EncodingName,
        absoluteUri);

      string textContent = null;
      if (binaryContent != null && binaryContent.Length > 0)
      {
        textContent = encoding.GetString(binaryContent);
      }

      return textContent;
    }
    #endregion
  }
}
