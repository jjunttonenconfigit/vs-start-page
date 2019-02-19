using StartPageEditor.Models;
using System.Collections.Concurrent;

namespace StartPageEditor.Helpers
{
  public interface IFileFinder
  {
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="FileFinder"/> is cancelled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if cancelled; otherwise, <c>false</c>.
    /// </value>
    bool Cancelled { get; set; }

    /// <summary>
    /// Cancel the find operation
    /// </summary>
    void Cancel();

    /// <summary>
    /// Find files
    /// </summary>
    /// <param name="paths">The paths.</param>
    /// <param name="patterns">The patterns.</param>
    /// <param name="directoryLevel">The directory level.</param>
    /// <returns></returns>
    ConcurrentBag<FileFinderResult> Find(string[] paths, string[] patterns, int directoryLevel);
  }
}