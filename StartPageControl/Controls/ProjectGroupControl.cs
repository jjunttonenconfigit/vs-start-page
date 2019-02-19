using StartPageControl.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StartPageControl.Controls
{
  /// <summary>
  /// ProjectGroupControl
  /// </summary>
  /// <seealso cref="System.Windows.Controls.GroupBox" />
  internal class ProjectGroupControl : Expander
  {
    private readonly Canvas _canvas;
    private int _rows;
    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    public string Title { get; set; }
    /// <summary>
    /// Gets or sets the index.
    /// </summary>
    /// <value>
    /// The index.
    /// </value>
    public int Index { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectGroupControl"/> class.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <param name="buttons">The buttons.</param>
    public ProjectGroupControl(RenderModel model,IEnumerable<ProjectButton> buttons = null)
    {
      this.Title = model.Name;
      this.Index = model.Index;
      this.Background = model.BackgroundBrush;
      this.Foreground = model.ForegroundBrush;
      this.Padding = new Thickness(0, 0, 0, 5);
      this.Width = LayoutConstants.ProjectGroupColumnWidth;
      this.IsExpanded = model.Expanded;
      this.Header = this.CreateHeader();
      _canvas = new Canvas
      {
        Background = model.BackgroundBrush,
      };
      if (buttons != null)
      {
        int count = 0;
        foreach (var button in buttons)
        {
          this.AddRow(button);
          count++;
        }
        _canvas.Height = count * LayoutConstants.ProjectRowHeight;

      }
      this.Content = _canvas;
    }
    /// <summary>
    /// Creates the header.
    /// </summary>
    /// <returns>HeaderedContentControl</returns>
    private HeaderedContentControl CreateHeader()
    {
      var headerPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
      };
      headerPanel.Children.Add(new Label
      {
        Padding = new Thickness(5,8,0,0),
        Content = this.Title,
        Foreground = this.Foreground ?? Brushes.Black,
        FontSize = 15,
      });
      var border = new Border
      {
        BorderBrush = this.Background,
        Background = this.Background,
        Child = headerPanel,
      };
      return new HeaderedContentControl
      {
        VerticalAlignment = VerticalAlignment.Center,
        Content = border,
        Height = LayoutConstants.ProjectGroupHeaderHeight,
      };
    }
    /// <summary>
    /// Adds the row.
    /// </summary>
    /// <param name="button">The button.</param>
    public void AddRow(ProjectButton button)
    {
      
      _canvas.Children.Add(button);
      Canvas.SetLeft(button, ((LayoutConstants.ProjectGroupColumnWidth - LayoutConstants.ButtonWidth) / 2) - 2);
      Canvas.SetTop(button, LayoutConstants.ProjectRowHeight * _rows);
      _rows++;
    }

    /// <summary>
    /// Clears this instance.
    /// </summary>
    public void Clear()
    {
      _canvas.Children.Clear();
    }
  }
}

