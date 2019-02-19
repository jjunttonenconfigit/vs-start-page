using StartPageControl.Helpers;
using StartPageEditor.Models;
using System.Windows;
using System.Windows.Forms;

namespace StartPageEditor.Helpers
{
  /// <summary>
  /// DialogService
  /// </summary>
  /// <seealso cref="StartPageEditor.Helpers.IDialogService" />
  public class DialogService : IDialogService
  {
    private readonly IJsonFileRepository<AppSettings> _appSettingsRepository;
    public DialogService(IJsonFileRepository<AppSettings> appSettingsRepository)
    {
      _appSettingsRepository = appSettingsRepository;
    }
    /// <summary>
    /// Messages the box.
    /// </summary>
    /// <param name="message">The message.</param>
    public void MessageBox(string message)
    {
      System.Windows.MessageBox.Show(message, "Notification", MessageBoxButton.OK);
    }


    /// <summary>
    /// Asks for confirmation.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns></returns>
    public bool AskForConfirmation(string message)
    {
      MessageBoxResult result = System.Windows.MessageBox.Show(message, "Are you sure?", MessageBoxButton.OKCancel);
      return result.HasFlag(MessageBoxResult.OK);
    }
    /// <summary>
    /// Folders the dialog.
    /// </summary>
    /// <returns></returns>
    public string FolderDialog()
    {
      string folder = string.Empty;
      using (var dialog = new FolderBrowserDialog())
      {
        var result = dialog.ShowDialog();
        dialog.ShowNewFolderButton = false;
        if (result == DialogResult.OK)
        {
          folder = dialog.SelectedPath;
        }
      }
      return folder;
    }
    /// <summary>
    /// Opens the file dialog.
    /// </summary>
    /// <returns></returns>
    public string OpenFileDialog(string filter = "")
    {
      OpenFileDialog openFileDialog = new OpenFileDialog
      {
        Filter = filter ?? "All files (*.*)|*.*",
      };
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        return openFileDialog.FileName;
      }
      return null;
    }

    /// <summary>
    /// Add project dialog.
    /// </summary>
    /// <returns></returns>
    public string AddProjectDialog()
    {
      return this.OpenFileDialog(this.GetProjectExtensionsFilter());
    }
    /// <summary>
    /// Save file dialog.
    /// </summary>
    /// <param name="defaultExtension">The default extension.</param>
    /// <param name="defaultFolder">The default folder.</param>
    /// <returns></returns>
    public string SaveFileDialog(string defaultExtension, string defaultFolder)
    {
      SaveFileDialog openFileDialog = new SaveFileDialog
      {
        DefaultExt = defaultExtension,
        InitialDirectory = defaultFolder,
        CheckFileExists = false,
        Filter = "Json files (*.json)|*.json",

      };
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        return openFileDialog.FileName;
      }
      return null;
    }

    /// <summary>
    /// Gets the project extensions filter for dialog.
    /// </summary>
    /// <returns></returns>
    private string GetProjectExtensionsFilter()
    {
      string filter = "";
      if (_appSettingsRepository.Data == null)
      {
        _appSettingsRepository.LoadFromString(StartPageEditor.Properties.Resources.DefaultSettings);
      }
      foreach (var e in _appSettingsRepository.Data.ProjectFileExtensions)
      {
        filter += $"{e.Description} files ({e.Extension}) |{e.Extension}|";
      }
      filter += "All files (*.*)|*.*";
      return filter;
    }
  }
}
