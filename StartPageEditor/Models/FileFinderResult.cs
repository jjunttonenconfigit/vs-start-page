using System.Collections.Concurrent;

namespace StartPageEditor.Models
{
  /// <summary>
  /// FileFinderResult
  /// </summary>
  public class FileFinderResult
  {
    /// <summary>
    /// Gets or sets the folder.
    /// </summary>
    /// <value>
    /// The folder.
    /// </value>
    public string Folder { get; set; }
    /// <summary>
    /// Gets or sets the file names.
    /// </summary>
    /// <value>
    /// The file names.
    /// </value>
    public ConcurrentBag<string> FileNames { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="FileFinderResult"/> class.
    /// </summary>
    public FileFinderResult()
    {
      this.FileNames = new ConcurrentBag<string>();
    }
  }
}
