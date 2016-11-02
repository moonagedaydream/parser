using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using ParserLibrary.HtmlParsers;

namespace WebCrawlerLibrary.SpecializedWebCrawlerHelper
{
  public class PagesDownloader
  {
    #region Private fields
    /// <summary>
    /// Logger.
    /// </summary>
    private static readonly ILog log = LogManager.GetLogger("WebCrawler");
    private readonly SpecializedCrawlerSettings settings = new SpecializedCrawlerSettings();
    private const int maxDepth = 50;
    #endregion

    #region Private methods.
    /// <summary>
    /// Process single URI.
    /// </summary>
    private void ProcessUrl(DownloadedPageInfo uriInfo, int depth)
    {
      log.InfoFormat("Processing URI '{0}', with depth '{1}'.", uriInfo.Uri.AbsoluteUri, depth);

      if (settings.Options.MaximumLinkDepth > 0 && depth > settings.Options.MaximumLinkDepth)
      {
        log.InfoFormat("Depth '{1}' exceeds maximum configured depth. Ending crawling at '{0}'.",
            uriInfo.Uri.AbsoluteUri,
            depth);
      }
      else if (depth > maxDepth)
      {
        log.InfoFormat("Depth {1} exceeds maximum allowed recursion depth. Ending recursion at URI '{0}' to possible continue later.",
            uriInfo.Uri.AbsoluteUri,
            depth);

        if (settings.HasDownloadedUri(uriInfo))
        {
          log.InfoFormat("URI '{0}' was already downloaded. Skipping.", uriInfo.Uri.AbsoluteUri);
        }
        else
        {
          settings.AddDownloadedResourceInfo(uriInfo);

          log.InfoFormat("Added URI '{0}' to continue later.", uriInfo.Uri.AbsoluteUri);
        }
      }
      else
      {
        // Async stuff.
        if (processAsyncBackgroundWorker != null)
        {
          if (processAsyncBackgroundWorker.CancellationPending)
          {
            throw new StopProcessingException();
          }
        }

        // Notify event about this URL.
        if (ProcessingUrl != null)
        {
          var e = new ProcessingUrlEventArgs(uriInfo, depth);

          ProcessingUrl(this, e);
        }

        if (settings.HasDownloadedUri(uriInfo))
        {
          log.InfoFormat("URI '{0}' was already downloaded. Skipping.", uriInfo.Uri.AbsoluteUri);
        }
        else
        {
          log.InfoFormat("URI '{0}' was not already downloaded. Processing.", uriInfo.Uri.AbsoluteUri);
          Encoding encoding;

          string textContent = SinglePageDownload.DownloadHtml(uriInfo.Uri, out encoding, settings.Options);

          var storer = new SinglePageSave(settings);

          var storedPage = storer.StoreHtml(textContent, encoding, uriInfo);

          HtmlParser parser = new AngleSharpParser();

          parser.Parse(storedPage.LocalFilePath.FullName);

          DatabaseSaver dbSaver = new DatabaseSaver(settings.Options.DownloadUri.ToString(), parser);
          dbSaver.ProcessPage(uriInfo.Uri);

          List<LinkInfo> links = parser.GetLinks(uriInfo.Uri.ToString())
              .Select(l => new LinkInfo(settings.Options, l)).ToList();

          foreach (LinkInfo link in links) {

              if (!dbSaver.IsExternal(link.Uri)) {
                  var downloadedPageInfo =
                      new DownloadedPageInfo(
                          link,
                          uriInfo.LocalFolderPath);

                  ProcessUrl(downloadedPageInfo, depth + 1);
              }
          }
            

          // Add before parsing childs.
          settings.AddDownloadedResourceInfo(uriInfo);

          // Persist after completely parsed childs.
          settings.PersistDownloadedResourceInfo(uriInfo);
        }

        log.InfoFormat("Finished processing URI '{0}'.", uriInfo.Uri.AbsoluteUri);
      }
    }
    #endregion

    #region Public methods.
    /// <summary>
    /// Initializes a new instance of the <see cref="PagesDownloader"/> 
    /// class.
    /// </summary>
    public PagesDownloader(CrawlerOptions options)
    {
      // Configure logger.
      XmlConfigurator.Configure();
      log.InfoFormat(
        "Constructing WebSiteDownloader for URI '{0}', destination folder path '{1}'.",
        options.DownloadUri,
        options.DestinationFolderPath);

      settings = SpecializedCrawlerSettings.Restore(options.DestinationFolderPath);

      settings.Options = options;
    }

    /// <summary>
    /// Performs the complete downloading (synchronously). 
    /// Returns only when completely finished or when an exception occured.
    /// </summary>
    public void Process()
    {
      var seedInfo =
        new DownloadedPageInfo(
          settings.Options,
          settings.Options.DownloadUri,
          settings.Options.DestinationFolderPath);

      if (!settings.HasContinueDownloadedPageInfo)
      {
        settings.AddContinueDownloadedPageInfo(seedInfo);
      }

      int index = 0;
      while (settings.HasContinueDownloadedPageInfo)
      {
        // Fetch one.
        DownloadedPageInfo processInfo = settings.PopContinueDownloadedPageInfo();

        log.InfoFormat("'{0}' loop: Starting processing URL '{1}'.",
            index + 1,
            processInfo.Uri.AbsoluteUri);

        ProcessUrl(processInfo, 0);
        index++;
      }

      log.InfoFormat("'{0}' loop: Finished processing URLs from seed '{1}'.",
          index + 1,
          settings.Options.DownloadUri);
    }

    /// <summary>
    /// Performs the complete downloading (asynchronously). 
    /// </summary>
    public void ProcessAsync()
    {
      processAsyncBackgroundWorker = new BackgroundWorker
      {
        WorkerSupportsCancellation = true
      };

      processAsyncBackgroundWorker.DoWork += processAsyncBackgroundWorker_DoWork;
      processAsyncBackgroundWorker.RunWorkerCompleted += processAsyncBackgroundWorker_RunWorkerCompleted;

      processAsyncBackgroundWorker.RunWorkerAsync();
    }

    /// <summary>
    /// Cancels a currently running async process.
    /// </summary>
    public void CancelProcessAsync()
    {
      if (processAsyncBackgroundWorker != null)
      {
        processAsyncBackgroundWorker.CancelAsync();
      }
    }

    #endregion

    #region Public events.
    /// <summary>
    /// Called when processing an URL.
    /// </summary>
    public event ProcessingUrlEventHandler ProcessingUrl;

    public delegate void ProcessingUrlEventHandler(
      object sender,
      ProcessingUrlEventArgs e);

    public class ProcessingUrlEventArgs : EventArgs
    {
      #region Private variables.
      private readonly DownloadedPageInfo uriInfo;
      private readonly int depth;
      #endregion

      #region Public methods.
      /// <summary>
      /// C-tor.
      /// </summary>
      internal ProcessingUrlEventArgs(
        DownloadedPageInfo uriInfo,
        int depth)
      {
        this.uriInfo = uriInfo;
        this.depth = depth;
      }
      #endregion

      #region Public properties.
      /// <summary>
      /// Depth.
      /// </summary>
      public int Depth
      {
        get { return depth; }
      }

      /// <summary>
      /// URI info.
      /// </summary>
      public DownloadedPageInfo UriInfo
      {
        get { return uriInfo; }
      }
      #endregion
    }
    #endregion

    #region Async stuff.
    private BackgroundWorker processAsyncBackgroundWorker = null;

    private void processAsyncBackgroundWorker_DoWork(
      object sender,
      DoWorkEventArgs e)
    {
      try
      {
        Process();
      }
      catch (StopProcessingException)
      {
        // Do nothing, just end.
      }
    }

    private void processAsyncBackgroundWorker_RunWorkerCompleted(
      object sender,
      RunWorkerCompletedEventArgs e)
    {
      if (ProcessCompleted != null)
      {
        ProcessCompleted(this, e);
      }
    }

    public delegate void ProcessCompletedEventHandler(
      object sender,
      RunWorkerCompletedEventArgs e);

    /// <summary>
    /// Called when the asynchron processing is completed.
    /// </summary>
    public event ProcessCompletedEventHandler ProcessCompleted;

    private class StopProcessingException : Exception
    {
    }
    #endregion
  }
}
