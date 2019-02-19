using StartPageEditor.Events;
using StartPageEditor.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace StartPageEditor.Controls
{
  /// <summary>
  /// Interaction logic for ProjectGroupTreeControl.xaml
  /// </summary>
  public partial class StartPageTreeControl : UserControl
  {
    public static readonly DependencyProperty SelectedItemProperty =
      DependencyProperty.Register("SelectedItem",
        typeof(object),
        typeof(StartPageTreeControl),
        new UIPropertyMetadata(null));

    public object SelectedItem
    {
      get => (object)GetValue(SelectedItemProperty);
      set => SetValue(SelectedItemProperty, value);
    }

    public StartPageTreeControl()
    {
      InitializeComponent();
    }

    private void StartPageTreeControl_Loaded(object sender, RoutedEventArgs e)
    {

      // this really should be a base level tree functionality but this code
      // solves the problem that moving a leaf node loses focus of the selected item
      if (this.DataContext != null)
      {
        if (this.DataContext is StartPageViewModel startPageViewModel)
        {
          startPageViewModel.ProjectMoved += OnProjectMoved;
        }
      }
    }
    private void OnProjectMoved(object sender, ProjectMovedEventArgs args)
    {
      var query = from ProjectGroupViewModel groupNode in StartPageTree.Items
                  where groupNode == args.ProjectGroupViewModel
                  select groupNode;
      var groupTreeItem = query.FirstOrDefault();
      TreeViewItem parent = (TreeViewItem)StartPageTree.ItemContainerGenerator.ContainerFromItem(groupTreeItem);
      if (parent.IsExpanded == false)
      {
        parent.ExpandSubtree();
      }
      var projectNode = (TreeViewItem)parent.ItemContainerGenerator.ContainerFromItem(args.ProjectViewModel);
      projectNode?.Focus();
    }
    // SelectedItem is readonly, this provides a manual binding
    private void StartPageTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
      SetValue(SelectedItemProperty, StartPageTree.SelectedItem);
    }

    // needs some work to support drag and drop 
    //private void StartPageTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    //{
    //  var model = ((ISelectable)StartPageTree.SelectedItem);
    //  if (Keyboard.IsKeyDown(Key.LeftCtrl))
    //  {
    //    model.IsSelected = !model.IsSelected;
    //  }
    //  else
    //  {
    //    var pageModel = this.DataContext as StartPageViewModel;

    //    foreach (var g in pageModel.ProjectGroups)
    //    {
    //      g.IsSelected = false;
    //      foreach (var p in g.Projects)
    //      {
    //        p.IsSelected = false;
    //      }
    //    }
    //    model.IsSelected = true;
    //  }
    //}
    //void startPageTree_MouseMove(object sender, MouseEventArgs e)
    //{
    //  if (!_isDragging && e.LeftButton == MouseButtonState.Pressed)
    //  {
    //    _isDragging = true;
    //    DragDrop.DoDragDrop(StartPageTree, StartPageTree.SelectedValue,
    //        DragDropEffects.Move);
    //  }
    //}
    //void startPageTree_DragOver(object sender, DragEventArgs e)
    //{
    //  if (e.Data.GetDataPresent(typeof(ProjectViewModel)))
    //  {
    //    e.Effects = DragDropEffects.Move;
    //  }
    //  else
    //  {
    //    e.Effects = DragDropEffects.None;
    //  }
    //}
    //void startPageTree_Drop(object sender, DragEventArgs e)
    //{
    //  if (e.Data.GetDataPresent(typeof(ProjectViewModel)))
    //  {
    //    ProjectViewModel sourceTask = (ProjectViewModel)e.Data.GetData(typeof(ProjectViewModel));
    //    ProjectViewModel targetTask = GetItemAtLocation<ProjectViewModel>(e.GetPosition(StartPageTree));
    //    _isDragging = false;
    //  }
    //}
    //private T GetItemAtLocation<T>(Point location)
    //{
    //  T foundItem = default(T);
    //  HitTestResult hitTestResults = VisualTreeHelper.HitTest(StartPageTree, location);

    //  if (hitTestResults.VisualHit is FrameworkElement)
    //  {
    //    object dataObject = (hitTestResults.VisualHit as
    //        FrameworkElement).DataContext;

    //    if (dataObject is T)
    //    {
    //      foundItem = (T)dataObject;
    //    }
    //  }
    //  return foundItem;
    //}
  }
}
