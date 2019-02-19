using StartPageEditor.Models;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StartPageEditor.Helpers
{
  /// <summary>
  /// FileFinder
  /// </summary>
  public class FileFinder : IFileFinder
  {
    private readonly IFileSystem _fileSystem;
    private CancellationTokenSource _cancelToken;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="FileFinder"/> is cancelled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if cancelled; otherwise, <c>false</c>.
    /// </value>
    public bool Cancelled { get; set; }

    //public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
    /// <summary>
    /// Initializes a new instance of the <see cref="FileFinder"/> class.
    /// </summary>
    public FileFinder(IFileSystem fileSystem)
    {
      _fileSystem = fileSystem;
    }

    /// <summary>
    /// Cancel the find operation
    /// </summary>
    public void Cancel()
    {
      _cancelToken.Cancel();
      this.Cancelled = true;
    }

    /// <summary>
    /// Find files
    /// </summary>
    /// <param name="paths">The paths.</param>
    /// <param name="patterns">The patterns.</param>
    /// <param name="directoryLevel">The directory level.</param>
    /// <returns></returns>
    public ConcurrentBag<FileFinderResult> Find(string[] paths, string[] patterns, int directoryLevel)
    {
      if (paths.Any(t => string.IsNullOrEmpty(t) == false) == false ||
          patterns.Any(t => string.IsNullOrEmpty(t) == false) == false)
      {
        throw new ArgumentException("Invalid parameters specified.  You must have at least 1 valid path and 1 pattern");
      }
      var results = new ConcurrentBag<FileFinderResult>();
      _cancelToken = new CancellationTokenSource();
      this.Cancelled = false;
      var parallelOptions = new ParallelOptions
      {
        CancellationToken = this._cancelToken.Token
      };

      Stopwatch watch = new Stopwatch();
      watch.Start();
      try
      {
        var directories = _fileSystem.GetDirectoriesAtLevel(paths, directoryLevel);

        Parallel.ForEach(directories, parallelOptions, (directory) =>
        {
          try
          {
            //ProgressChanged?.Invoke(this, new ProgressChangedEventArgs(currentFile, count / directories.Count());

            var result = new FileFinderResult {Folder = directory};
            var dirs = _fileSystem.GetDirectoriesRecursive(directory);
            Parallel.ForEach(dirs, parallelOptions, (dir) =>
            {
              foreach (string pattern in patterns)
              {
                var files = _fileSystem.GetFileMatches(dir, pattern)?.OrderBy(s => s);
                if (files != null)
                {
                  foreach (string file in files)
                  {
                    result.FileNames.Add(file);
                  }
                }
              }
            });
            if (result.FileNames.Count > 0)
            {
              results.Add(result);
            }
          }
          catch (OperationCanceledException)
          {
          }
          catch (Exception)
          {
          }
        });
      }
      catch (OperationCanceledException)
      {
      }
      watch.Stop();
      Debug.Print($"Generate time: {watch.ElapsedMilliseconds}");
      return results;
    }
  }
}

