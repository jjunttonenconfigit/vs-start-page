namespace StartPageControl.Helpers
{
  /// <summary>
  /// IJsonFileRepository interface
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IJsonFileRepository<T> where T : class
  {
    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    T Data { get; set; }
    /// <summary>
    /// Gets or sets the name of the file.
    /// </summary>
    /// <value>
    /// The name of the file.
    /// </value>
    string FileName { get; set; }
    /// <summary>
    /// Gets or sets the folder.
    /// </summary>
    /// <value>
    /// The folder.
    /// </value>
    string Folder { get; set; }
    /// <summary>
    /// Loads this instance.
    /// </summary>
    /// <returns></returns>
    T Load();
    /// <summary>
    /// Loads the specified file.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns></returns>
    T Load(string file);
    /// <summary>
    /// Loads from string.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns></returns>
    T LoadFromString(string text);
    /// <summary>
    /// Saves this instance.
    /// </summary>
    void Save();
    /// <summary>
    /// Saves the specified model.
    /// </summary>
    /// <param name="model">The model.</param>
    void Save(T model);
    /// <summary>
    /// Saves as.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <param name="file">The file.</param>
    void SaveAs(T model, string file);
  }
}