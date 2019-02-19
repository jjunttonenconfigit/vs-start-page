using System.Collections.ObjectModel;

namespace StartPageEditor.Models
{
  /// <summary>
  /// Project group model
  /// </summary>
  /// <seealso cref="StartPageEditor.Models.TreeViewItemModel" />
  public class ProjectGroupViewModel : TreeViewItemModel
  {
    private string _name;
    private int _index;
    private bool _expanded;

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name
    {
      get => _name;
      set
      {
        _name = value;
        NotifyPropertyChanged("Name");
      }
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public int Index
    {
      get => _index;
      set
      {
        _index = value;
        NotifyPropertyChanged("Index");
      }
    }
    /// <summary>
    /// Gets or sets expanded.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public bool Expanded
    {
      get => _expanded;
      set
      {
        _expanded = value;
        NotifyPropertyChanged("Expanded");
      }
    }

    /// <summary>
    /// Gets or sets the background.
    /// </summary>
    /// <value>
    /// The background.
    /// </value>
    private string _background;
    public string Background
    {
      get => _background;
      set
      {
        _background = value;
        NotifyPropertyChanged("Background");
      }
    }

    /// <summary>
    /// Gets or sets the foreground.
    /// </summary>
    /// <value>
    /// The foreground.
    /// </value>
    private string _foreground;
    public string Foreground
    {
      get => _foreground;
      set
      {
        _foreground = value;
        NotifyPropertyChanged("Foreground");
      }
    }
    /// <summary>
    /// Gets or sets the projects.
    /// </summary>
    /// <value>
    /// The projects.
    /// </value>
    public ObservableCollection<ProjectViewModel> Projects { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectGroupViewModel"/> class.
    /// </summary>
    public ProjectGroupViewModel()
    {
      this.Projects = new ObservableCollection<ProjectViewModel>();
    }

  }
}