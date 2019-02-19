using System.Collections.Generic;

namespace StartPageControl.Models
{
  /// <summary>
  /// Project group model
  /// </summary>
  public class ProjectGroup
  {
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the background.
    /// </summary>
    /// <value>
    /// The background.
    /// </value>
    public string Background { get; set; }
    /// <summary>
    /// Gets or sets the background.
    /// </summary>
    /// <value>
    /// The background.
    /// </value>
    public string Foreground { get; set; }
    /// <summary>
    /// Gets or sets the projects.
    /// </summary>
    /// <value>
    /// The projects.
    /// </value>
    public IList<Project> Projects { get; set; }

    /// <summary>
    /// Gets or sets the index.
    /// </summary>
    /// <value>
    /// The index.
    /// </value>
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets the IsExpanded
    /// </summary>
    public bool Expanded { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectGroup"/> class.
    /// </summary>
    public ProjectGroup()
    {
      this.Projects = new List<Project>();
    }
  }
}