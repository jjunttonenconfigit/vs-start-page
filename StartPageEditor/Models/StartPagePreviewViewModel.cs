using StartPageControl;
using StartPageControl.Commands;
using StartPageControl.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace StartPageEditor.Models
{
  /// <summary>
  /// 
  /// </summary>
  /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
  public class StartPagePreviewViewModel : INotifyPropertyChanged
  {
    private readonly StartPageViewerViewModel _startPageViewerViewModel;
    private List<string> _startPageFiles;
    private string _selectedFile;

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets or sets the scale up command.
    /// </summary>
    /// <value>
    /// The scale up command.
    /// </value>
    public ICommand ScaleUpCommand { get; set; }
    /// <summary>
    /// Gets or sets the scale down command.
    /// </summary>
    /// <value>
    /// The scale down command.
    /// </value>
    public ICommand ScaleDownCommand { get; set; }

    /// <summary>
    /// Gets or sets the start page files.
    /// </summary>
    /// <value>
    /// The start page files.
    /// </value>
    public List<string> StartPageFiles
    {
      get
      {
        if(_startPageFiles == null)
        {
          _startPageFiles = new List<string>();
          foreach (var file in StartPageEnvironment.GetStartPageFiles())
          {
            _startPageFiles.Add(file);
          }
        }
        return _startPageFiles;
      }
      set
      {
        _startPageFiles = value;
        NotifyPropertyChanged("StartPageFiles");
      }
    }
    /// <summary>
    /// Gets or sets the selected file.
    /// </summary>
    /// <value>
    /// The selected file.
    /// </value>
    public string SelectedFile
    {
      get => _selectedFile;
      set
      {
        _startPageViewerViewModel.LoadFromFile(value);
        _selectedFile = value;
        NotifyPropertyChanged("SelectedFile");
      }
    }
    /// <summary>
    /// Sets the selected model.
    /// </summary>
    /// <value>
    /// The selected model.
    /// </value>
    public StartPageModel SelectedModel
    {
      set => _startPageViewerViewModel.LoadFromModel(value);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="StartPagePreviewViewModel"/> class.
    /// </summary>
    /// <param name="startPageViewerViewModel">The start page viewer view model.</param>
    public StartPagePreviewViewModel(StartPageViewerViewModel startPageViewerViewModel)
    {
        _startPageViewerViewModel = startPageViewerViewModel;
      this.ScaleUpCommand = new RelayCommand(null, () => { _startPageViewerViewModel.ScaleUp(); });
      this.ScaleDownCommand = new RelayCommand(null, () => { _startPageViewerViewModel.ScaleDown(); });

    }
    /// <summary>
    /// Notifies the property changed.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    private void NotifyPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

}
