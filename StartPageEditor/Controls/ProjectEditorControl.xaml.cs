using StartPageEditor.Models;
using System.Windows;
using System.Windows.Controls;

namespace StartPageEditor.Controls
{
  /// <summary>
  /// Interaction logic for ProjectEditorControl.xaml
  /// </summary>
  public partial class ProjectEditorControl : UserControl
  {
    private ProjectGroupViewModel _selectedGroup;
    /// <summary>
    /// Dependency property to inject selected Project treeview item
    /// </summary>
    public static readonly DependencyProperty ProjectProperty =
         DependencyProperty.Register("Project", typeof(ProjectViewModel),
         typeof(ProjectEditorControl), new FrameworkPropertyMetadata(null));

    /// <summary>
    /// The selected project from the treeview 
    /// </summary>
    public ProjectViewModel Project
    {
      get => (ProjectViewModel)GetValue(ProjectProperty);
      set => SetValue(ProjectProperty, value);
    }
    /// <summary>
    /// Gets or sets the selected group.
    /// </summary>
    /// <value>
    /// The selected group.
    /// </value>
    public ProjectGroupViewModel SelectedGroup
    {
      get => _selectedGroup;
      set
      {
        if (value != null)
        {
          if (this.DataContext is StartPageViewModel startPageViewModel)
          {
            startPageViewModel.MoveProject(this.Project, (ProjectGroupViewModel) value);
            _selectedGroup = null;
            ProjectGroupList.SelectedItem = null;
          }
        }

      }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectEditorControl"/> class.
    /// </summary>
    public ProjectEditorControl()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Move the project to the selected project group
    /// TODO: this shouldn't be here
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
    private void ProjectGroupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var projectsCombo = (sender as ComboBox);
      if (projectsCombo?.SelectedItem != null)
      {
        if (this.DataContext is StartPageViewModel startPageViewModel)
        {
          startPageViewModel.MoveProject(this.Project,
            (ProjectGroupViewModel) projectsCombo.SelectedItem);
          projectsCombo.SelectedItem = null;
        }
      }
      
    }
  }
}
