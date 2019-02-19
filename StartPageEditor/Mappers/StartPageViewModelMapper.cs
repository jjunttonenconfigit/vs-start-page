using StartPageEditor.Models;
using StartPageControl.Models;

namespace StartPageEditor.Mappers
{
  /// <summary>
  /// StartPageViewModelMapper
  /// </summary>
  public static class StartPageViewModelMapper
  {
    /// <summary>
    /// Map from a StartPageModel
    /// </summary>
    /// <param name="fromPage">From page.</param>
    /// <param name="toPage">To page.</param>
    public static void MapFrom(StartPageModel fromPage, StartPageViewModel toPage)
    {
      toPage.ProjectGroups.Clear();
      foreach (var group in fromPage.ProjectGroups)
      {
        toPage.ProjectGroups.Add(ProjectGroupViewModelMapper.MapFrom(group));
      }
    }
    /// <summary>
    /// Map to a StartPageModel
    /// </summary>
    /// <param name="fromPage">From page.</param>
    /// <returns></returns>
    public static StartPageModel MapTo(StartPageViewModel fromPage)
    {
      var model = new StartPageModel();
      foreach (var group in fromPage.ProjectGroups)
      {
        var projectGroupModel = new ProjectGroup
        {
          Name = group.Name,
          Background = group.Background,
          Foreground = group.Foreground,
          Index = group.Index,
          Expanded = group.Expanded,
        };
        foreach (var project in group.Projects)
        {
          projectGroupModel.Projects.Add(new Project
          {
            Name = project.Name,
            Location = project.Location,
            Index = project.Index
          });
        }
        model.ProjectGroups.Add(projectGroupModel);
      }
      return model;
    }
  }
}
