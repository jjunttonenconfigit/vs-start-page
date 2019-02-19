using StartPageControl.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StartPageEditor.Helpers
{
  /// <summary>
  /// StartPageGenerator
  /// </summary>
  public class StartPageGenerator : IStartPageGenerator
  {
    private readonly IFileFinder _fileFinder;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartPageGenerator"/> class.
    /// </summary>
    public StartPageGenerator(IFileFinder fileFinder)
    {
      //_fileFinder = new FileFinder();
      _fileFinder = fileFinder;
    }
    /// <summary>
    /// Cancels this instance.
    /// </summary>
    public void Cancel()
    {
      _fileFinder.Cancel();
    }
    /// <summary>
    /// Gets a value indicating whether this <see cref="IStartPageGenerator" /> is cancelled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if cancelled; otherwise, <c>false</c>.
    /// </value>
    public bool Cancelled => _fileFinder.Cancelled;

    /// <summary>
    /// Generate a start page from the specified folders using the extensions which are setup in the configuration form
    /// </summary>
    /// <param name="folders">The folders.</param>
    /// <returns></returns>
    public async Task<StartPageModel> GenerateAsync(string[] folders)
    {
      var appSettingsRepository = new AppSettingsRepository();
      var data = appSettingsRepository.LoadSettings();

      var matches = await Task.Run(() => _fileFinder.Find(folders, appSettingsRepository.SelectedExtensions, data.GroupAtDirectoryLevel));
      
      if (_fileFinder.Cancelled)
      {
        return null;
      }
      var model = new StartPageModel();
      if (matches.Any())
      {
        int projectGroupIndex = 1;
        foreach (var match in matches.OrderBy(t => new DirectoryInfo(t.Folder).Name))
        {
          int projectIndex = 1;
          var projects = match
            .FileNames
            .OrderBy(Path.GetFileName)
            .Select(r => new Project
            {
              Name = Path.GetFileName(r),
              Location = r,
              Index = projectIndex++
            })
            .OrderBy(f => f.Location)
            .ToList();

          model.ProjectGroups.Add(new ProjectGroup
          {
            Name = new DirectoryInfo(match.Folder).Name,
            Background = "WhiteSmoke",
            Foreground = "Black",
            Projects = projects,
            Expanded = true,
            Index = projectGroupIndex
          });
          projectGroupIndex++;
        }
      }
      return model;
    }
  }
}
