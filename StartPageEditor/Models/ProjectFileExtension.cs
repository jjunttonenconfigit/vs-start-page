namespace StartPageEditor.Models
{
  /// <summary>
  /// ProjectFileExtension
  /// </summary>
  public class ProjectFileExtension
  {
    /// <summary>
    /// Gets or sets the extension.
    /// </summary>
    /// <value>
    /// The extension.
    /// </value>
    public string Extension { get; set; }
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    public string Description { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this instance is selected.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
    /// </value>
    public bool IsSelected { get; set; }
  }
}
