using StartPageControl.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace StartPageControl.Controls
{
  /// <summary>
  /// StartPageCanvas
  /// </summary>
  /// <seealso cref="System.Windows.Controls.Canvas" />
  internal class StartPageCanvas : Canvas
  {
    /// <summary>
    /// Renders the specified render models.
    /// </summary>
    /// <param name="renderModels">The render models.</param>
    /// <param name="maxRowHeight">Maximum height of the row.</param>
    /// <returns></returns>
    public StartPageCanvas Render(IEnumerable<RenderModel> renderModels, double maxRowHeight)
    {
      this.Clear();
      if (renderModels != null)
      {
        double left = LayoutConstants.ProjectGroupMargin;
        double top = LayoutConstants.ProjectGroupMargin;
        foreach (var renderModel in renderModels.OrderBy(t => t.Index))
        {
          this.RenderGroup(renderModel, maxRowHeight, ref left, ref top);
        }
        this.Width = left + LayoutConstants.ProjectGroupColumnWidth + LayoutConstants.ProjectGroupMargin +
                     LayoutConstants.ProjectGroupColumnGutter;
        this.Height = maxRowHeight;
      }
      return this;
    }
    /// <summary>
    /// Renders the group.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <param name="maxRowHeight">Maximum height of the row.</param>
    /// <param name="left">The left.</param>
    /// <param name="top">The top.</param>
    public void RenderGroup(RenderModel model, double maxRowHeight, ref double left, ref double top)
    {
      double height = LayoutConstants.ProjectGroupHeaderHeight + LayoutConstants.ProjectGroupMargin;

      bool DoesRowFit(double value)
      {
        return value + LayoutConstants.ProjectRowHeight <= maxRowHeight - LayoutConstants.ProjectGroupRowGutter;
      }
      
      if (model.Expanded)
      {
        var buttons = new List<ProjectButton>();
        if (model.ProjectButtons.Any())
        {
          foreach (var button in model.ProjectButtons?.OrderBy(t => t.Index))
          {
            if (DoesRowFit(top + height) == false)
            {
              if (buttons.Count > 0)
              {
                this.AddGroupControl(model, buttons, left, top);
                buttons.Clear();
                height = LayoutConstants.ProjectGroupHeaderHeight + LayoutConstants.ProjectGroupMargin;
              }
              top = LayoutConstants.ProjectGroupMargin;
              left += LayoutConstants.ProjectGroupColumnWidth + LayoutConstants.ProjectGroupMargin;
            }
            buttons.Add(button);
            height += LayoutConstants.ProjectRowHeight;
          }
        }
        this.AddGroupControl(model, buttons, left, top);
      }
      else
      {
        if (DoesRowFit(top + height) == false)
        {
          top = LayoutConstants.ProjectGroupMargin;
          left += LayoutConstants.ProjectGroupColumnWidth + LayoutConstants.ProjectGroupMargin;
        }
        this.AddGroupControl(model, null, left, top);
      }
      top += height + LayoutConstants.ProjectGroupMargin;
    }
    /// <summary>
    /// Renders the group control.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <param name="buttons">The buttons.</param>
    /// <param name="left">The left.</param>
    /// <param name="top">The top.</param>
    public void AddGroupControl(RenderModel model, IEnumerable<ProjectButton> buttons, double left, double top)
    {
      var control = new ProjectGroupControl(model, buttons);
      this.Children.Add(control);
      Canvas.SetLeft(control, left);
      Canvas.SetTop(control, top);
    }
    /// <summary>
    /// Clears this instance.
    /// </summary>
    public void Clear()
    {
      foreach (var child in this.Children)
      {
        if (child is ProjectGroupControl groupControl)
        {
          groupControl.Clear();
        }
      }
      this.Children.Clear();
    }
  }
}
