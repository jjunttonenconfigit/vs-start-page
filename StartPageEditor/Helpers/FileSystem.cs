using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StartPageEditor.Helpers
{
  public class FileSystem : IFileSystem
  {
    /// <summary>
    /// Gets the directories at level.
    /// </summary>
    /// <param name="paths">The paths.</param>
    /// <param name="directoryLevel">The directory level.</param>
    /// <returns></returns>
    public IEnumerable<string> GetDirectoriesAtLevel(string[] paths, int directoryLevel)
    {
      if (directoryLevel == 0)
      {
        return paths;
      }
      var dirs = new List<string>();
      foreach (string path in paths)
      {
        var folders = Directory.GetDirectories(path);
        for (int t = 1; t < directoryLevel; t++)
        {
          folders = this.GetSubDirectories(folders).ToArray();
        }
        dirs.AddRange(folders);
      }

      return dirs;
    }

    /// <summary>
    /// Gets the sub directories.
    /// </summary>
    /// <param name="directories">The directories.</param>
    /// <returns></returns>
    public IEnumerable<string> GetSubDirectories(string[] directories)
    {
      foreach (var dir in directories)
      {
        foreach (var subDir in Directory.EnumerateDirectories(dir))
        {
          yield return subDir;
        }
      }
    }

    /// <summary>
    /// Gets the directories recursive.
    /// </summary>
    /// <param name="dir">The dir.</param>
    /// <returns></returns>
    public IEnumerable<string> GetDirectoriesRecursive(string dir)
    {
      var dirs = new List<string> { dir };
      foreach (string folder in Directory.EnumerateDirectories(dir))
      {
        dirs.AddRange(this.GetDirectoriesRecursive(folder));
      }
      return dirs;
    }
    /// <summary>
    /// get matching files for a path/pattern combination
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="pattern">The pattern.</param>
    /// <returns></returns>
    public IEnumerable<string> GetFileMatches(string path, string pattern)
    {
      if (Directory.Exists(path))
      {
        foreach (string file in Directory.EnumerateFiles(path, pattern))
        {
          yield return file;
        }
      }
    }

    /// <summary>
    /// Files the exists.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns></returns>
    public bool FileExists(string file)
    {
      return File.Exists(file);
    }
  }
}
