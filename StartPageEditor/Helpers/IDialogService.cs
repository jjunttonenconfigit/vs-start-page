namespace StartPageEditor.Helpers
{
  /// <summary>
  /// IDialogService
  /// </summary>
  public interface IDialogService
  {
    /// <summary>
    /// Messagesbox.
    /// </summary>
    /// <param name="message">The message.</param>
    void MessageBox(string message);
    /// <summary>
    /// Open file dialog.
    /// </summary>
    /// <returns></returns>
    string OpenFileDialog(string filter);
    /// <summary>
    /// Folder dialog.
    /// </summary>
    /// <returns></returns>
    string FolderDialog();
    /// <summary>
    /// Add project dialog.
    /// </summary>
    /// <returns></returns>
    string AddProjectDialog();
    /// <summary>
    /// Saves file dialog.
    /// </summary>
    /// <param name="defaultExtension">The default extension.</param>
    /// <param name="defaultFolder">The default folder.</param>
    /// <returns></returns>
    string SaveFileDialog(string defaultExtension, string defaultFolder);
    /// <summary>
    /// Asks for confirmation.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns></returns>
    bool AskForConfirmation(string message);
  }
}
