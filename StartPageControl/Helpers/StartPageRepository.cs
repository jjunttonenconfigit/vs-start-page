using StartPageControl.Models;
using System.Collections.Generic;
using System.Linq;

namespace StartPageControl.Helpers
{
  /// <summary>
  /// StartPageRepository
  /// </summary>
  /// <seealso cref="StartPageModel" />
  public class StartPageRepository : JsonFileRepository<StartPageModel>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="StartPageRepository"/> class.
    /// </summary>
    public StartPageRepository()
    {
      this.Folder = StartPageEnvironment.StartPageFolder;
    }
    /// <summary>
    /// Updates the specified render models.
    /// </summary>
    /// <param name="renderModels">The render models.</param>
    internal void Update(IEnumerable<RenderModel> renderModels)
    {
      var pairs = from m in this.Data.ProjectGroups
                  join d in renderModels
                  on m.Index equals d.Index
                  select new { m, d };
      foreach (var pair in pairs)
      {
        pair.m.Expanded = pair.d.Expanded;
      }
      this.Save();
    }
  }
}
