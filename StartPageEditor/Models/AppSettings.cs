using System.Collections.Generic;

namespace StartPageEditor.Models
{
  /// <summary>
  /// AppSettings model
  /// </summary>
  public class AppSettings
  {
    /// <summary>
    /// Gets or sets the project file extensions.
    /// </summary>
    /// <value>
    /// The project file extensions.
    /// </value>
    public List<ProjectFileExtension> ProjectFileExtensions { get; set; }
    /// <summary>
    /// Gets or sets the group at directory level.
    /// </summary>
    /// <value>
    /// The group at directory level.
    /// </value>
    public int GroupAtDirectoryLevel { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppSettings"/> class.
    /// </summary>
    public AppSettings()
    {
      this.ProjectFileExtensions = new List<ProjectFileExtension>();
    }
  }
}
