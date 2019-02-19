using System.Windows;
using StartPageControl.Models;
using System.Windows.Controls;

namespace StartPageControl.Controls
{
  /// <summary>
  /// Project button - launches open project
  /// </summary>
  /// <seealso cref="System.Windows.Controls.Button" />
  internal class ProjectButton : Button
  {
    /// <summary>
    /// Gets or sets the index.
    /// </summary>
    /// <value>
    /// The index.
    /// </value>
    public int Index { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectButton"/> class.
    /// </summary>
    /// <param name="project">The project.</param>
    public ProjectButton(Project project)
    {
      this.Index = project.Index;
      this.Height = LayoutConstants.ButtonHeight;
      this.Width = LayoutConstants.ButtonWidth;
      this.Content = project.Name;
      this.Tag = project.Location;
      this.HorizontalAlignment = HorizontalAlignment.Center;
    }
  }
}
