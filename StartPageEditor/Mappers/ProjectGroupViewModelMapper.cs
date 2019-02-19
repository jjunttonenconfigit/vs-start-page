using StartPageEditor.Models;
using StartPageControl.Models;

namespace StartPageEditor.Mappers
{
  /// <summary>
  /// ProjectGroupViewModelMapper
  /// </summary>
  public static class ProjectGroupViewModelMapper
  {
    /// <summary>
    /// Map from a ProjectGroup
    /// </summary>
    /// <param name="fromGroup">From group.</param>
    /// <returns></returns>
    public static ProjectGroupViewModel MapFrom(ProjectGroup fromGroup)
    {
      var group = new ProjectGroupViewModel();
      group.Name = fromGroup.Name;
      group.Background = fromGroup.Background;
      group.Foreground = fromGroup.Foreground;
      group.Index = fromGroup.Index;
      group.Expanded = fromGroup.Expanded;
      foreach (var p in fromGroup.Projects)
      {
        group.Projects.Add(new ProjectViewModel
        {
          Name = p.Name,
          Index = p.Index,
          Location = p.Location,
        });
      }
      return group;
    }
  }
}
