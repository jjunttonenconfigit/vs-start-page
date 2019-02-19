using System.Collections.Generic;

namespace StartPageControl.Models
{
  /// <summary>
  /// Start page model
  /// </summary>
  public class StartPageModel
  {
    /// <summary>
    /// Gets or sets the project groups.
    /// </summary>
    /// <value>
    /// The project groups.
    /// </value>
    public IList<ProjectGroup> ProjectGroups { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StartPageModel"/> class.
    /// </summary>
    public StartPageModel()
    {
      this.ProjectGroups = new List<ProjectGroup>();
    }

  }
}
