using StartPageEditor.Models;

namespace StartPageEditor.Events
{
  /// <summary>
  /// ProjectMovedEventArgs
  /// </summary>
  public class ProjectMovedEventArgs
  {
    /// <summary>
    /// Gets or sets the project view model.
    /// </summary>
    /// <value>
    /// The project view model.
    /// </value>
    public ProjectViewModel ProjectViewModel { get; set; }
    /// <summary>
    /// Gets or sets the project group view model.
    /// </summary>
    /// <value>
    /// The project group view model.
    /// </value>
    public ProjectGroupViewModel ProjectGroupViewModel { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectMovedEventArgs"/> class.
    /// </summary>
    /// <param name="groupViewModel">The group view model.</param>
    /// <param name="projectViewModel">The project view model.</param>
    public ProjectMovedEventArgs(ProjectGroupViewModel groupViewModel, ProjectViewModel projectViewModel)
    {
      this.ProjectViewModel = projectViewModel;
      this.ProjectGroupViewModel = groupViewModel;
    }
  }
}
