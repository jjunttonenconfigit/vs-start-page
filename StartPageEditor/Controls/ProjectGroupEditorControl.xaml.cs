using StartPageEditor.Events;
using StartPageEditor.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StartPageEditor.Controls
{
  /// <summary>
  /// Interaction logic for ProjectGroupEditorControl.xaml
  /// </summary>
  public partial class ProjectGroupEditorControl : UserControl
  {
    /// <summary>
    /// ProjectGroupProperty 
    /// </summary>
    public static readonly DependencyProperty ProjectGroupProperty =
         DependencyProperty.Register("ProjectGroup", typeof(ProjectGroupViewModel),
         typeof(ProjectGroupEditorControl), new FrameworkPropertyMetadata(null));

    /// <summary>
    /// The selected ProjectGroup in the treeview
    /// </summary>
    public ProjectGroupViewModel ProjectGroup
    {
      get => (ProjectGroupViewModel)GetValue(ProjectGroupProperty);
      set => SetValue(ProjectGroupProperty, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectGroupEditorControl"/> class.
    /// </summary>
    public ProjectGroupEditorControl()
    {
      InitializeComponent();
    }
  }
}
