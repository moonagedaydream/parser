using System;
using System.IO;
using System.Text;
using log4net;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper
{
  internal sealed class SinglePageSave
  {
    #region Private fields.
    /// <summary>
    /// Logger.
    /// </summary>
    private static readonly ILog log = LogManager.GetLogger("WebCrawler");
    private readonly SpecializedCrawlerSettings settings;
    #endregion

    #region Public methods.
    /// <summary>
    /// C-tor.
    /// </summary>
    public SinglePageSave(SpecializedCrawlerSettings settings)
    {
      this.settings = settings;
    }

    /// <summary>
    /// Download html page locally.
    /// </summary>
    public DownloadedPageInfo StoreHtml(
      string textContent,
      Encoding encoding,
      LinkInfo uriInfo)
    {
      var result =
        new DownloadedPageInfo(
          uriInfo,
          this.settings.Options.DestinationFolderPath);

      try
      {
        if (result.LocalFilePath.Exists)
        {
          result.LocalFilePath.Delete();
        }

        if (result.LocalFilePath.Directory != null && !result.LocalFilePath.Directory.Exists)
        {
          result.LocalFilePath.Directory.Create();
        }

        log.InfoFormat("Creating file '{0}'.", result.LocalFilePath);

        using (var stream = new FileStream(
          result.LocalFilePath.FullName,
          FileMode.Create,
          FileAccess.Write))
        {
          using (var writer = new StreamWriter(stream, encoding))
          {
            writer.Write(textContent);
          }
        }
      }
      catch (Exception exception)
      {
        log.Error(exception);
      }

      return result;
    }
    #endregion
  }
}
