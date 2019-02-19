using StartPageControl.Controls;
using StartPageControl.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace StartPageControl.Builders
{
  /// <summary>
  /// Render model builder.  Build cached GUI objects
  /// </summary>
  internal class RenderModelsBuilder
  {
    /// <summary>
    /// Builds the render models cache
    /// </summary>
    /// <param name="startPageModel">The start page model.</param>
    /// <returns></returns>
    public List<RenderModel> Build(StartPageModel startPageModel)
    {
      var models = new List<RenderModel>();
      var brushConverter = new BrushConverter();
      foreach (var projectGroup in startPageModel.ProjectGroups.OrderBy(t => t.Index))
      {
        var model = new RenderModel
        {
          BackgroundBrush = (SolidColorBrush) (brushConverter.ConvertFrom(projectGroup.Background ?? "white")),
          ForegroundBrush = (SolidColorBrush) (brushConverter.ConvertFrom(projectGroup.Foreground ?? "black")),
          Name = projectGroup.Name,
          Index = projectGroup.Index,
          Expanded = projectGroup.Expanded,
        };
        foreach (var project in projectGroup.Projects.OrderBy(t => t.Index))
        {
          model.ProjectButtons.Add(new ProjectButton(project));
        }
        models.Add(model);
      }
      return models;
    }
  }
}

