using Microsoft.VisualStudio.Shell;
using StartPageControl.Controls;
using StartPageControl.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace StartPageControl
{
  /// <summary>
  /// Interaction logic for StartPageViewerControl.xaml
  /// </summary>
  /// 
  public partial class StartPageViewerControl : UserControl
  {
    private readonly StartPageViewerViewModel _startPageViewerViewModel;

    /// <summary>
    /// The FileName property
    /// </summary>
    public static readonly DependencyProperty FileNameProperty =
         DependencyProperty.Register("FileName", typeof(string),
         typeof(StartPageViewerControl), new FrameworkPropertyMetadata(null));

    /// <summary>
    /// The page model property
    /// </summary>
    public static readonly DependencyProperty PageModelProperty =
     DependencyProperty.Register("PageModel", typeof(StartPageModel),
     typeof(StartPageViewerControl), new FrameworkPropertyMetadata(null));

    /// <summary>
    /// The page model property
    /// </summary>
    public static readonly DependencyProperty LoadVisualStudioProperty =
     DependencyProperty.Register("LoadVisualStudio", typeof(bool),
     typeof(StartPageViewerControl), new FrameworkPropertyMetadata(null));

    /// <summary>
    /// Gets or sets LoadVisualStudioProperty - if true a new instance of Visual Studio will be started.
    /// </summary>
    /// <value>
    /// The name of the file.
    /// </value>
    public bool LoadVisualStudio
    {
      get
      {
        return (bool)GetValue(LoadVisualStudioProperty);
      }
      set
      {
        SetValue(LoadVisualStudioProperty, value);
      }
    }
    /// <summary>
    /// Gets or sets the name of the file.
    /// </summary>
    /// <value>
    /// The name of the file.
    /// </value>
    public string FileName
    {
      get
      {
        return (string)GetValue(FileNameProperty);
      }
      set
      {
        SetValue(FileNameProperty, value);
        if (string.IsNullOrEmpty(value) == false)
        {
          _startPageViewerViewModel.LoadFromFile(value);
        }
      }
    }
    /// <summary>
    /// Gets or sets the page model.
    /// </summary>
    /// <value>
    /// The page model.
    /// </value>
    public StartPageModel PageModel
    {
      get
      {
        return (StartPageModel)GetValue(PageModelProperty);
      }
      set
      {
        SetValue(PageModelProperty, value);
        if(value != null)
        {
          _startPageViewerViewModel.LoadFromModel(value);
        }
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StartPageViewerControl"/> class.
    /// </summary>
    public StartPageViewerControl()
    {
      InitializeComponent();
      _startPageViewerViewModel = (StartPageViewerViewModel)this.DataContext;

    }
    //TODO: implement system.windows.interactivity to handle these events in an MVVM way
    /// <summary>
    /// Handles the Loaded event of the StartPageViewerControl control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    public void StartPageViewerControl_Loaded(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrEmpty(this.FileName) == false)
      {
        _startPageViewerViewModel.LoadFromFile(this.FileName);
      }
    }

    // TODO: Can get rid of these 2 methods by using system.windows.interactivity EventToCommand, leaving for now to avoid additional dependencies

    //private void ProjectButton_Click(object sender, RoutedEventArgs e)
    //{
    //  if (e.OriginalSource is ProjectButton == false)
    //  {
    //    return;
    //  }
    //  var button = e.OriginalSource as ProjectButton;
    //  string fileName = button.Tag.ToString();
    //  string command;
    //  var projectExtensions = new List<string> { ".sln", ".csproj" };
    //  if (projectExtensions.Contains(System.IO.Path.GetExtension(fileName)))
    //  {
    //    command = "File.OpenProject";
    //  }
    //  else
    //  {
    //    command = "File.OpenFile";
    //  }
    //  e.Handled = true;
    //  VSCommands.ExecuteCommand.Execute($"{command} \"{fileName}\"", null);
    //}

    /// <summary>
    /// Handles the Click event of the ProjectButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    private void ProjectButton_Click(object sender, RoutedEventArgs e)
    {
      if (e.OriginalSource is ProjectButton == false)
      {
        return;
      }
      var button = e.OriginalSource as ProjectButton;
      string fileName = button.Tag.ToString();
      e.Handled = true;
      if (this.LoadVisualStudio)
      {
        Process.Start(fileName);
      }
      else
      {
        VSCommands.ExecuteCommand.Execute($"File.OpenProject \"{fileName}\"", null);
      }
    }
    /// <summary>
    /// Called when 5[project group expanded].
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    private void OnProjectGroupExpanded(object sender, RoutedEventArgs e)
    {
      _startPageViewerViewModel.ExpandProjectGroupControl(sender as ProjectGroupControl, e.RoutedEvent.Name == "Expanded");
    }

  }
}

