namespace StartPageEditor.Models
{
  /// <summary>
  /// Project model
  /// </summary>
  public class ProjectViewModel : TreeViewItemModel
  {
    private string _name;
    private string _location;
    private int _index;
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
    /// Gets or sets the index.
    /// </summary>
    /// <value>
    /// The index.
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
    /// Gets or sets the location.
    /// </summary>
    /// <value>
    /// The location.
    /// </value>
    public string Location
    {
      get => _location;
      set
      {
        _location = value;
        NotifyPropertyChanged("Location");
      }
    }
  }
}
