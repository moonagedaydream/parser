using System;
using System.Diagnostics;
using System.IO;
using log4net;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper
{
  /// <summary>
  /// Information about a downloaded page.
  /// </summary>
  [Serializable]
  public class DownloadedPageInfo : LinkInfo, IEquatable<DownloadedPageInfo>
  {
    #region Private variables.
    /// <summary>
    /// Logger.
    /// </summary>
    private static readonly ILog log = LogManager.GetLogger("WebCrawler");

    private readonly DirectoryInfo localFolderPath;
    private readonly FileInfo localFilePath;
    private readonly FileInfo localFileName;

    private readonly int addedByProcessID = Process.GetCurrentProcess().Id;
    private readonly DateTime dateAdded = DateTime.Now;
    #endregion

    #region Private methods.
    /// <summary>
    /// Makes the name of the local file.
    /// </summary>
    private static FileInfo MakeLocalFileName(Uri uri)
    {
      if (!uri.IsAbsoluteUri)
      {
        throw new ArgumentException(String.Format("URI {0} must be absolute but it is not.", uri));
      }

      // Each URI is unique.
      string unique = uri.AbsoluteUri.GetHashCode().ToString(@"X8");

      var result = new FileInfo(unique + ".html");

      log.InfoFormat("Making local file path '{0}' for URI '{1}'.",
          result.FullName,
          uri.AbsoluteUri);

      return result;
    }
    #endregion

    #region Public properties.
    /// <summary>
    /// Gets the local folder path.
    /// </summary>
    public DirectoryInfo LocalFolderPath
    {
      get { return localFolderPath; }
    }

    /// <summary>
    /// Gets the local file path.
    /// </summary>
    public FileInfo LocalFilePath
    {
      get { return localFilePath; }
    }

    /// <summary>
    /// Gets the name of the local file.
    /// </summary>
    public FileInfo LocalFileName
    {
      get { return localFileName; }
    }

    /// <summary>
    /// Gets the added by process ID.
    /// </summary>
    public int AddedByProcessID
    {
      get { return addedByProcessID; }
    }

    /// <summary>
    /// Gets the date added.
    /// </summary>
    public DateTime DateAdded
    {
      get { return dateAdded; }
    }

    #endregion

    #region Public methods.
    /// <summary>
    /// C-tor.
    /// </summary>
    public DownloadedPageInfo(
      LinkInfo copyFrom,
      DirectoryInfo folderPath)
      : base(copyFrom)
    {
      this.localFolderPath = folderPath;

      this.localFilePath = new FileInfo(
        Path.Combine(
          localFolderPath.FullName,
          MakeLocalFileName(copyFrom.Uri).Name));

      localFileName = new FileInfo(localFilePath.Name);
    }

    /// <summary>
    /// C-tor.
    /// </summary>
    public DownloadedPageInfo(
      CrawlerOptions options,
      Uri uri,
      DirectoryInfo folderPath)
      : base(options, uri)
    {
      this.localFolderPath = folderPath;
    }

    /// <summary>
    /// Gets a value indicating whether file exists.
    /// </summary>
    public bool FileExists
    {
      get
      {
        FileInfo fileName = MakeLocalFileName(Uri);
        var filePath = new FileInfo(Path.Combine(localFolderPath.FullName, fileName.Name));
        return filePath.Exists;
      }
    }
    #endregion

    #region IEquatable<DownloadedPageInfo> implementation.
    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    public bool Equals(DownloadedPageInfo other)
    {
      if (Uri == null && other.Uri == null)
      {
        return true;
      }
      if (Uri == null || other.Uri == null)
      {
        return false;
      }
      int result =
        String.Compare(Uri.AbsoluteUri, other.Uri.AbsoluteUri,
          StringComparison.OrdinalIgnoreCase);

      return result == 0;
    }
    #endregion
  }
}
