using StartPageControl.Controls;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace StartPageControl.Models
{
  internal class RenderModel
  {
    /// <summary>
    /// Gets or sets the index.
    /// </summary>
    /// <value>
    /// The index.
    /// </value>
    public int Index { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="RenderModel"/> is expanded.
    /// </summary>
    /// <value>
    ///   <c>true</c> if expanded; otherwise, <c>false</c>.
    /// </value>
    public bool Expanded { get; set; }
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    /// <value>
    /// The height.
    /// </value>
    public double Height { get; set; }
    /// <summary>
    /// Gets or sets the project buttons.
    /// </summary>
    /// <value>
    /// The project buttons.
    /// </value>
    public Collection<ProjectButton> ProjectButtons { get; set; }
    /// <summary>
    /// Gets or sets the background brush.
    /// </summary>
    /// <value>
    /// The background brush.
    /// </value>
    public SolidColorBrush BackgroundBrush { get; set; }
    /// <summary>
    /// Gets or sets the foreground brush.
    /// </summary>
    /// <value>
    /// The foreground brush.
    /// </value>
    public SolidColorBrush ForegroundBrush { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="RenderModel"/> class.
    /// </summary>
    public RenderModel()
    {
      this.ProjectButtons = new Collection<ProjectButton>();
    }
  }
}
