using System;
using System.Collections.Generic;
using System.IO;

namespace StartPageControl
{
  /// <summary>
  /// StartPageEnvironment
  /// </summary>
  public static class StartPageEnvironment
  {
    /// <summary>
    /// The start page folder
    /// </summary>
    public static string StartPageFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Visual Studio 2017\StartPages";
    /// <summary>
    /// The application settings file, initially stored as a resource but saved to the same folder as the EXE once changes are saved
    /// </summary>
    public static string AppSettingsFile = $@"{AppDomain.CurrentDomain.BaseDirectory}\AppSettings.json";
    /// <summary>
    /// Gets the start page files.
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<string> GetStartPageFiles()
    {
      var files = Directory.GetFiles(StartPageEnvironment.StartPageFolder, "*.json");
      foreach (var file in files)
      {
        yield return Path.GetFileName(file);
      }
    }
  }
}
