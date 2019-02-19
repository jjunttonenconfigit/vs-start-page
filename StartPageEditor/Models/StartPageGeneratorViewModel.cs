using StartPageControl.Commands;
using StartPageControl.Models;
using StartPageEditor.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace StartPageEditor.Models
{
  /// <summary>
  /// StartPageGeneratorViewModel
  /// </summary>
  /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
  public class StartPageGeneratorViewModel : INotifyPropertyChanged
  {
    private readonly IStartPageGenerator _startPageGenerator;
    private readonly IDialogService _dialogService;
    private bool _isBusy;
    /// <summary>
    /// Gets or sets the folders.
    /// </summary>
    /// <value>
    /// The folders.
    /// </value>
    public ObservableCollection<string> Folders { get; set; }
    /// <summary>
    /// Gets or sets the generated model.
    /// </summary>
    /// <value>
    /// The generated model.
    /// </value>
    public StartPageModel GeneratedModel { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether [create empty].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [create empty]; otherwise, <c>false</c>.
    /// </value>
    public bool CreateEmpty { get; set; }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
    /// <summary>
    /// Gets or sets the create empty page command.
    /// </summary>
    /// <value>
    /// The create empty page command.
    /// </value>
    public ICommand CreateEmptyPageCommand { get; set; }
    /// <summary>
    /// Gets or sets the generate command.
    /// </summary>
    /// <value>
    /// The generate command.
    /// </value>
    public ICommand GenerateCommand{ get; set; }
    /// <summary>
    /// Gets or sets the cancel generation command.
    /// </summary>
    /// <value>
    /// The cancel generation command.
    /// </value>
    public ICommand CancelGenerationCommand { get; set; }
    /// <summary>
    /// Gets or sets the add folder command.
    /// </summary>
    /// <value>
    /// The add folder command.
    /// </value>
    public ICommand AddFolderCommand { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this instance is cancelling.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is cancelling; otherwise, <c>false</c>.
    /// </value>
    public bool IsCancelling { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this instance is busy.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
    /// </value>
    public bool IsBusy
    {
      get => _isBusy;
      set
      {
        _isBusy = value;
        NotifyPropertyChanged("IsBusy");
      }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="StartPageGeneratorViewModel"/> class.
    /// </summary>
    /// <param name="dialogService">The dialog service.</param>
    /// <param name="startPageGenerator">The start page generator.</param>
    public StartPageGeneratorViewModel(IDialogService dialogService, IStartPageGenerator startPageGenerator)
    {
      _dialogService = dialogService;
      _startPageGenerator = startPageGenerator;
      this.CreateEmptyPageCommand = new RelayCommand<ICloseable>(null, (window) => { this.CreateEmptyPage(window); });
      this.GenerateCommand = new RelayCommand<ICloseable>((o) => { return this.IsBusy == false && this.Folders.Count > 0; }, (window) => { this.Generate(window); });
      this.CancelGenerationCommand = new RelayCommand(() => { return this.IsBusy == true && this.IsCancelling == false; }, () => { this.CancelGeneration(); });
      this.AddFolderCommand = new RelayCommand(null, () => { this.AddFolder(); });

      this.Folders = new ObservableCollection<string>();
      
    }

    /// <summary>
    /// Adds the folder.
    /// </summary>
    private void AddFolder()
    {
      string folder = _dialogService.FolderDialog();
      if(string.IsNullOrEmpty(folder) == false)
      { 
          this.Folders.Add(folder);
      }
    }

    /// <summary>
    /// Generates the specified window.
    /// </summary>
    /// <param name="window">The window.</param>
    private async void Generate(ICloseable window)
    {
      this.IsBusy = true;
      var startPageModel = await _startPageGenerator.GenerateAsync(this.Folders.ToArray());
      if (_startPageGenerator.Cancelled || startPageModel == null || startPageModel?.ProjectGroups.Count == 0)
      {
        _dialogService.MessageBox(_startPageGenerator.Cancelled ? "Cancelled!" : "No projects found");
        this.IsBusy = false;
        this.IsCancelling = false;
        return;
      }
      this.IsBusy = false;
      this.GeneratedModel = startPageModel;
      _dialogService.MessageBox("Completed");
      ((ICloseable) window)?.Close();
    }

    /// <summary>
    /// Cancels the generation.
    /// </summary>
    private void CancelGeneration()
    {
      this.IsCancelling = true;
      _startPageGenerator.Cancel();
    }

    /// <summary>
    /// Creates the empty page.
    /// </summary>
    /// <param name="window">The window.</param>
    private void CreateEmptyPage(ICloseable window)
    {
      this.CreateEmpty = true;
      ((ICloseable) window)?.Close();
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
