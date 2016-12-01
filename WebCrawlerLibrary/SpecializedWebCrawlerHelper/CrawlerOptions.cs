using System;
using System.Configuration;
using System.IO;
using log4net;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper
{
  /// <summary>
  /// Crawler options.
  /// </summary>
  [Serializable]
  public class CrawlerOptions
  {
    #region Private fields
    /// <summary>
    /// Logger.
    /// </summary>
    private static readonly ILog log = LogManager.GetLogger("WebCrawler");

    /// <summary>
    /// Uri.
    /// </summary>
    private Uri downloadUri;

    /// <summary>
    /// Folder to store pages locally.
    /// </summary>
    private DirectoryInfo destinationFolderPath;

    /// <summary>
    /// Maximum depth for crawler.
    /// </summary>
    private int maximumDepth = 100500;
    #endregion

    #region C-tor
    /// <summary>
		/// Initializes a new instance of the
    /// <see cref="CrawlerOptions"/> class.
		/// </summary>
    public CrawlerOptions()
    {
      string congigurationDepth = ConfigurationManager.AppSettings[@"maximumDepth"];
      if (!String.IsNullOrEmpty(congigurationDepth))
			{
        maximumDepth = Convert.ToInt32(congigurationDepth);
			}
      log.DebugFormat("Maximum depth = '{0}'", maximumDepth);
		}
    #endregion

    #region Public properties.
    /// <summary>
    /// Gets or sets the download URI.
    /// </summary>
    public Uri DownloadUri
    {
      get { return downloadUri; }
      set { downloadUri = value; }
    }

    /// <summary>
    /// Gets or sets the destination folder path.
    /// </summary>
    public DirectoryInfo DestinationFolderPath
    {
      get
      {
        if (destinationFolderPath != null && !destinationFolderPath.Exists)
        {
          destinationFolderPath.Create();
        }

        return destinationFolderPath;
      }
      set { destinationFolderPath = value; }
    }

    /// <summary>
    /// Gets or sets the link depth.
    /// </summary>
    public int MaximumLinkDepth
    {
      get { return maximumDepth; }
      set { maximumDepth = value; }
    }
    #endregion
  }
}
