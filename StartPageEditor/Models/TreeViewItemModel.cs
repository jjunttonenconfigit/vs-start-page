using StartPageEditor.Helpers;
using System.ComponentModel;

namespace StartPageEditor.Models
{
  /// <summary>
  /// TreeViewItemModel
  /// </summary>
  /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
  /// <seealso cref="StartPageEditor.Helpers.ISelectable" />
  public class TreeViewItemModel : INotifyPropertyChanged, ISelectable
  {
    private bool _isSelected;
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets or sets selected.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public bool IsSelected
    {
      get => _isSelected;
      set
      {
        _isSelected = value;
        NotifyPropertyChanged("IsSelected");
      }
    }

    /// <summary>
    /// Notifies the property changed.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    public virtual void NotifyPropertyChanged(string propertyName)
    {
      if (propertyName != "IsSelected")  
      {
        DirtyTracker.IsDirty = true;  // one way dirty
      }
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


  }
}
