using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper
{
  /// <summary>
  /// Class to store as the persistent state of the downloader.
  /// </summary>
  [Serializable]
  public class SpecializedCrawlerSettings
  {
    #region Private fields.
    /// <summary>
    /// Logger.
    /// </summary>
    private static readonly ILog log = LogManager.GetLogger("WebCrawler");
    private const string stateFileName = "WebSiteDownloader.state";

    [NonSerialized]
    private CrawlerOptions options = new CrawlerOptions();
    
    private List<DownloadedPageInfo> temporaryDownloadedPageInfo =
      new List<DownloadedPageInfo>();

    private List<DownloadedPageInfo> persistentDownloadedPageInfo =
      new List<DownloadedPageInfo>();

    /// <summary>
    /// The URLs where to continue parsing when the stack trace gets too deep.
    /// </summary>

    private List<DownloadedPageInfo> continueDownloadedPageInfo =
      new List<DownloadedPageInfo>();

    #endregion

    #region Public properties.
    /// <summary>
    /// Crawler options.
    /// </summary>
    public CrawlerOptions Options
    {
      get { return options; }
      set { options = value; }
    }

    /// <summary>
    /// Gets a value indicating whether instance has continue downloaded page infos.
    /// </summary>
    public bool HasContinueDownloadedPageInfo
    {
      get { return continueDownloadedPageInfo.Count > 0; }
    }
    #endregion

    #region Public methods.
    /// <summary>
    /// Persist settings.
    /// </summary>
    private void Persist()
    {
      try
      {
        string filePath = Path.Combine(
          options.DestinationFolderPath.FullName,
          stateFileName);

        if (File.Exists(filePath))
        {
          File.Delete(filePath);
        }
        log.InfoFormat(
          "Persisting crawler settings to file '{0}'. '{1}' temporary downloaded resources, '{2}' persistent downloaded resources, '{3}' continue downloaded resources.",
          filePath,
          temporaryDownloadedPageInfo.Count,
          persistentDownloadedPageInfo.Count,
          continueDownloadedPageInfo.Count);

        var serializer = new BinaryFormatter();
        using (var writer = new FileStream(
          filePath,
          FileMode.CreateNew,
          FileAccess.Write))
        {
          serializer.Serialize(
            writer,
            this);
        }
      }
      catch (Exception x)
      {
        log.Error(x);
      }
    }

    /// <summary>
    /// Restore a previously stored setting value from the given folder path.
    /// </summary>
    /// <returns>Returns an empty object if not found.</returns>
    public static SpecializedCrawlerSettings Restore(DirectoryInfo folderPath)
    {
      string filePath = Path.Combine(
        folderPath.FullName,
        stateFileName);

      if (File.Exists(filePath))
      {
        try
        {
          var serializer = new BinaryFormatter();
          using (var reader = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read))
          {
            var settings = (SpecializedCrawlerSettings) serializer.Deserialize(reader);

            settings.Options = new CrawlerOptions {DestinationFolderPath = folderPath};

            if (settings.temporaryDownloadedPageInfo == null)
            {
              settings.temporaryDownloadedPageInfo =
                new List<DownloadedPageInfo>();
            }
            if (settings.persistentDownloadedPageInfo == null)
            {
              settings.persistentDownloadedPageInfo =
                new List<DownloadedPageInfo>();
            }
            if (settings.continueDownloadedPageInfo == null)
            {
              settings.continueDownloadedPageInfo =
                new List<DownloadedPageInfo>();
            }

            settings.temporaryDownloadedPageInfo.Clear();
            settings.temporaryDownloadedPageInfo.AddRange(settings.persistentDownloadedPageInfo);

            log.InfoFormat(
              "Successfully restored crawler settings from file '{0}'. '{1}' temporary downloaded resources, '{2}' persistent downloaded resources, '{3}' continue downloaded resources.",
              filePath,
              settings.temporaryDownloadedPageInfo.Count,
              settings.persistentDownloadedPageInfo.Count,
              settings.continueDownloadedPageInfo.Count);

            return settings;
          }
        }
        catch (Exception x)
        {
          log.Error(x);
          var settings = new SpecializedCrawlerSettings
          {
            Options = {DestinationFolderPath = folderPath}
          };

          return settings;
        }
      }
      else
      {
        var settings = new SpecializedCrawlerSettings
        {
          Options = {DestinationFolderPath = folderPath}
        };

        return settings;
      }
    }

    /// <summary>
    /// Check whether a file was already downloaded.
    /// </summary>
    public bool HasDownloadedUri(DownloadedPageInfo uriInfo)
    {

      int foundPosition = temporaryDownloadedPageInfo.IndexOf(uriInfo);

      if (foundPosition < 0)
      {
        return false;
      }
      // Found. Check various attributes.
      DownloadedPageInfo foundInfo = temporaryDownloadedPageInfo[foundPosition];

      if (foundInfo.AddedByProcessID == Process.GetCurrentProcess().Id)
      {
        return true;
      }
      if (foundInfo.DateAdded.AddHours(10) > DateTime.Now)
      {
        return true;
      }
      return foundInfo.FileExists;
    }

    /// <summary>
    /// Add information about downloaded page.
    /// </summary>
    public void AddDownloadedResourceInfo(DownloadedPageInfo info)
    {
      if (temporaryDownloadedPageInfo.Contains(info))
      {
        temporaryDownloadedPageInfo.Remove(info);
      }

      temporaryDownloadedPageInfo.Add(info);
    }

    /// <summary>
    /// Persist information about downloaded page.
    /// </summary>
    public void PersistDownloadedResourceInfo(DownloadedPageInfo uriInfo)
    {
      int foundPosition = temporaryDownloadedPageInfo.IndexOf(uriInfo);
      DownloadedPageInfo foundInfo = temporaryDownloadedPageInfo[foundPosition];

      if (persistentDownloadedPageInfo.Contains(foundInfo))
      {
        persistentDownloadedPageInfo.Remove(foundInfo);
      }

      persistentDownloadedPageInfo.Add(foundInfo);

      Persist();
    }

    /// <summary>
    /// The URLs where to continue parsing when the stack trace gets too deep.
    /// </summary>
    public void AddContinueDownloadedPageInfo(DownloadedPageInfo resourceInfo)
    {
      if (continueDownloadedPageInfo.Contains(resourceInfo))
      {
        continueDownloadedPageInfo.Remove(resourceInfo);
      }

      continueDownloadedPageInfo.Add(resourceInfo);
      Persist();
    }

    /// <summary>
    /// Pops the continue downloaded resource infos.
    /// </summary>
    public DownloadedPageInfo PopContinueDownloadedPageInfo()
    {
      if (continueDownloadedPageInfo.Count <= 0)
      {
        return null;
      }
      DownloadedPageInfo result = continueDownloadedPageInfo[0];

      continueDownloadedPageInfo.RemoveAt(0);

      Persist();

      return result;
    }

    #endregion
  }
}
