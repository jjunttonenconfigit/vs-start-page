using StartPageControl.Commands;
using StartPageEditor.Helpers;
using System.Windows.Input;

namespace StartPageEditor.Models
{
  /// <summary>
  /// StartPageConfiguratorViewModel
  /// </summary>
  public class StartPageConfiguratorViewModel
  {
    private readonly AppSettingsRepository _repository;

    /// <summary>
    /// Gets or sets the application settings.
    /// </summary>
    /// <value>
    /// The application settings.
    /// </value>
    public AppSettings AppSettings { get; set; }

    /// <summary>
    /// Gets or sets the save configuration command.
    /// </summary>
    /// <value>
    /// The save configuration command.
    /// </value>
    public ICommand SaveConfigurationCommand { get; set; }

    /// <summary>
    /// The is dirty
    /// </summary>
    public bool IsDirty = true;
    /// <summary>
    /// Initializes a new instance of the <see cref="StartPageConfiguratorViewModel"/> class.
    /// </summary>
    public StartPageConfiguratorViewModel()
    {
      _repository = new AppSettingsRepository();
      this.AppSettings = _repository.LoadSettings();

      this.SaveConfigurationCommand = new RelayCommand<ICloseable>(null, this.SaveConfiguration);
    }
    /// <summary>
    /// Saves the configuration.
    /// </summary>
    /// <param name="win">The win.</param>
    public void SaveConfiguration(ICloseable win)
    {
      _repository.Save(this.AppSettings);
      ((ICloseable) win)?.Close();
    }
  }
}
