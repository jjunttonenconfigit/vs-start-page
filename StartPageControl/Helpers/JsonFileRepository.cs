using Newtonsoft.Json;
using System;
using System.IO;

namespace StartPageControl.Helpers
{
  /// <summary>
  /// JsonFileRepository
  /// Generic Json file repository
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <seealso cref="StartPageControl.Helpers.IJsonFileRepository{T}" />
  public class JsonFileRepository<T> : IJsonFileRepository<T> where T : class
  {
    private T _data;
    private readonly JsonSerializerSettings _serializerSettings;

    /// <summary>
    /// Gets or sets the folder.
    /// </summary>
    /// <value>
    /// The folder.
    /// </value>
    public string Folder { get; set; }
    /// <summary>
    /// Gets or sets the name of the file.
    /// </summary>
    /// <value>
    /// The name of the file.
    /// </value>
    public string FileName { get; set; }
    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public T Data
    {
      get => _data;
      set => _data = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonFileRepository{T}"/> class.
    /// </summary>
    /// <param name="serializerSettings">The serializer settings.</param>
    public JsonFileRepository(JsonSerializerSettings serializerSettings)
    {
      _serializerSettings = serializerSettings;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonFileRepository{T}"/> class.
    /// </summary>
    public JsonFileRepository()
    {
      _serializerSettings = new JsonSerializerSettings
      {
        Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
        {
          args.ErrorContext.Handled = true;
          throw new Exception(args.ErrorContext.Error.Message);
        }
      };
    }

    /// <summary>
    /// Loads this instance.
    /// </summary>
    /// <returns></returns>
    public T Load() 
    {
      return Load(this.FileName);
    }
    /// <summary>
    /// Loads the specified file.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns></returns>
    public virtual T Load(string file)
    {
      this.FileName = file;
      if(string.IsNullOrEmpty(this.Folder) == false)
      {
        file = Path.Combine(this.Folder, file);
      }
      if (File.Exists(file))
      {
        string text = File.ReadAllText(file);
        _data = JsonConvert.DeserializeObject<T>(text, _serializerSettings);
      }
      else
      {
        //throw new FileNotFoundException($"{file} was not found.");
      }
      return _data;
    }
    /// <summary>
    /// Loads from string.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns></returns>
    public T LoadFromString(string text)
    {
      _data = JsonConvert.DeserializeObject<T>(text, _serializerSettings);
      return _data;
    }

    /// <summary>
    /// Saves this instance.
    /// </summary>
    public virtual void Save()
    {
      this.Save(this.Data);
    }
    /// <summary>
    /// Saves the specified file.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <exception cref="FileNotFoundException">There is no file specified.</exception>
    public virtual void Save(T model)
    {
      if(string.IsNullOrEmpty(this.FileName))
      {
        throw new FileNotFoundException("There is no file specified.");
      }
      this.SaveAs(model, this.FileName);
    }
    /// <summary>
    /// Saves as.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <param name="file">The file.</param>
    public virtual void SaveAs(T model, string file)
    {
      if(string.IsNullOrEmpty(this.Folder) == false)
      {
        file = Path.Combine(this.Folder, file);
      }
      using (StreamWriter fileWriter = File.CreateText(file))
      {
        JsonSerializer serializer = new JsonSerializer
        {
          Formatting = Formatting.None
        };
        serializer.Error += delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
        {
          args.ErrorContext.Handled = true;
          throw new Exception(args.ErrorContext.Error.Message);
        };
        serializer.Serialize(fileWriter, model);
        _data = model;
      }
    }
  }
}
