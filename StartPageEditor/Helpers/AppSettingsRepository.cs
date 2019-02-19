using StartPageControl;
using StartPageControl.Helpers;
using StartPageEditor.Models;
using System.Linq;

namespace StartPageEditor.Helpers
{
  /// <summary>
  /// ProjectFileExtensionRepository
  /// avoid the dependency on appsettings.json by loading default from the resource file, 
  /// on first save it will be written to the executing folder
  /// </summary>
  public class AppSettingsRepository : JsonFileRepository<AppSettings>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AppSettingsRepository"/> class.
    /// </summary>
    public AppSettingsRepository()
    {
      this.FileName = StartPageEnvironment.AppSettingsFile;
    }

    /// <summary>
    /// Loads the settings.
    /// </summary>
    /// <returns></returns>
    public AppSettings LoadSettings()
    {
      if (this.Load() == null)
      {
        this.Data = this.LoadFromString(StartPageEditor.Properties.Resources.DefaultSettings);
      }

      return this.Data;
    }
    /// <summary>
    /// Get the selected file extensions setup in the configure form
    /// </summary>
    /// <value>
    /// The selected extensions.
    /// </value>
    /// <exception cref="System.Exception">You must call load before accessing the repository's data</exception>
    public string[] SelectedExtensions
    {
      get
      {
        if (this.Data == null)
        {
          throw new System.Exception("You must call load before accessing the repository's data");
        }
        return this.Data.ProjectFileExtensions
          .Where(f => f.IsSelected)
          .Select(f => f.Extension)
          .ToArray();
      }
    }
  }
}