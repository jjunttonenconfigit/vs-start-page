using System.Collections.Generic;

namespace StartPageEditor.Helpers
{
  public interface IFileSystem
  {
    /// <summary>
    /// Gets the directories at level.
    /// </summary>
    /// <param name="paths">The paths.</param>
    /// <param name="directoryLevel">The directory level.</param>
    /// <returns></returns>
    IEnumerable<string> GetDirectoriesAtLevel(string[] paths, int directoryLevel);

    /// <summary>
    /// Gets the sub directories.
    /// </summary>
    /// <param name="directories">The directories.</param>
    /// <returns></returns>
    IEnumerable<string> GetSubDirectories(string[] directories);

    /// <summary>
    /// Gets the directories recursive.
    /// </summary>
    /// <param name="dir">The dir.</param>
    /// <returns></returns>
    IEnumerable<string> GetDirectoriesRecursive(string dir);

    /// <summary>
    /// get matching files for a path/pattern combination
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="pattern">The pattern.</param>
    /// <returns></returns>
    IEnumerable<string> GetFileMatches(string path, string pattern);

    /// <summary>
    /// Files the exists.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns></returns>
    bool FileExists(string file);
  }
}

