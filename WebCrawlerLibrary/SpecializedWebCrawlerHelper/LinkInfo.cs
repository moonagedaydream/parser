using System;
using log4net;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper
{
  /// <summary>
  /// Information about link.
  /// </summary>
  [Serializable]
  public class LinkInfo
  {
    #region Private fields.
    /// <summary>
    /// Logger.
    /// </summary>
    private static readonly ILog log = LogManager.GetLogger("WebCrawler");
    private readonly CrawlerOptions options;
    private readonly Uri uri;
    #endregion

    #region Public properties.
    /// <summary>
    /// Gets the URI.
    /// </summary>
    public Uri Uri
    {
      get { return uri; }
    }
    #endregion

    #region Public methods.
		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="LinkInfo"/> class.
		/// </summary>
		public LinkInfo(CrawlerOptions options, Uri uri)
		{
		  this.options = options;
		  this.uri = uri;
		}

    /// <summary>
		/// Initializes a new instance of the 
		/// <see cref="LinkInfo"/> class.
		/// </summary>
    public LinkInfo(LinkInfo copyFrom)
		{
      this.options = copyFrom.options;
      this.uri = copyFrom.uri;
		}
		#endregion
  }
}
