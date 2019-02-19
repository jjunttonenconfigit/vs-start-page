using System;

namespace StartPageEditor.Events
{
  /// <summary>
  /// ProgressChangedEventArgs
  /// </summary>
  /// <seealso cref="System.EventArgs" />
  public class ProgressChangedEventArgs : EventArgs
  {
    /// <summary>
    /// Gets or sets the percent complete.
    /// </summary>
    /// <value>
    /// The percent complete.
    /// </value>
    public int PercentComplete { get; set; }
    /// <summary>
    /// Gets or sets the current file.
    /// </summary>
    /// <value>
    /// The current file.
    /// </value>
    public string CurrentFile { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="ProgressChangedEventArgs"/> class.
    /// </summary>
    /// <param name="currentFile">The current file.</param>
    /// <param name="percentComplete">The percent complete.</param>
    public ProgressChangedEventArgs(string currentFile, int percentComplete)
    {
      this.CurrentFile = currentFile;
      this.PercentComplete = percentComplete;
    }
  }
}
