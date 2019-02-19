using StartPageControl;
using StartPageControl.Commands;
using StartPageControl.Models;
using StartPageEditor.Events;
using StartPageEditor.Helpers;
using StartPageEditor.Mappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using StartPageControl.Helpers;

namespace StartPageEditor.Models
{
  /// <summary>
  /// Start page view model - the view model for the start page editor form
  /// </summary>
  public class StartPageViewModel : INotifyPropertyChanged
  {
    private string _selectedStartPage;
    private readonly IStartPageGenerator _startPageGenerator;
    private readonly IJsonFileRepository<StartPageModel> _startPageRepository;
    private bool _isBusy;

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Occurs when a project is moved
    /// </summary>
    public event EventHandler<ProjectMovedEventArgs> ProjectMoved;

    /// <summary>
    /// Gets or sets the selected item.
    /// </summary>
    /// <value>
    /// The selected item.
    /// </value>
    public object SelectedItem { get; set; }

    /// <summary>
    /// Gets or sets the add project command.
    /// </summary>
    /// <value>
    /// The add project command.
    /// </value>
    public ICommand AddProjectCommand { get; set; }
    /// <summary>
    /// Gets or sets the delete project command.
    /// </summary>
    /// <value>
    /// The delete project command.
    /// </value>
    public ICommand DeleteProjectCommand { get; set; }
    /// <summary>
    /// Gets or sets the delete project group command.
    /// </summary>
    /// <value>
    /// The delete project group command.
    /// </value>
    public ICommand DeleteProjectGroupCommand { get; set; }
    /// <summary>
    /// Gets or sets the create page command.
    /// </summary>
    /// <value>
    /// The create page command.
    /// </value>
    public ICommand CreatePageCommand { get; set; }
    /// <summary>
    /// Gets or sets the save page command.
    /// </summary>
    /// <value>
    /// The save page command.
    /// </value>
    public ICommand SavePageCommand { get; set; }
    /// <summary>
    /// Gets or sets the preview page command.
    /// </summary>
    /// <value>
    /// The preview page command.
    /// </value>
    public ICommand PreviewPageCommand { get; set; }
    /// <summary>
    /// Gets or sets the add group from folder command.
    /// </summary>
    /// <value>
    /// The add group from folder command.
    /// </value>
    public ICommand AddGroupFromFolderCommand { get; set; }
    /// <summary>
    /// Gets or sets the add empty group command.
    /// </summary>
    /// <value>
    /// The add empty group command.
    /// </value>
    public ICommand AddEmptyGroupCommand { get; set; }
    /// <summary>
    /// Gets or sets the add group button dropdown command.
    /// </summary>
    /// <value>
    /// The add group button dropdown command.
    /// </value>
    public ICommand AddGroupButtonDropdownCommand { get; set; }
    /// <summary>
    /// Gets or sets the move selected tree item command.
    /// </summary>
    /// <value>
    /// The move selected tree item command.
    /// </value>
    public ICommand MoveSelectedTreeItemCommand { get; set; }
    /// <summary>
    /// Gets or sets the configure form command.
    /// </summary>
    /// <value>
    /// The configure form command.
    /// </value>
    public ICommand ConfigureFormCommand { get; set; }
    /// <summary>
    /// Gets or sets the start page files.
    /// </summary>
    /// <value>
    /// The start page files.
    /// </value>
    public ObservableCollection<string> StartPageFiles { get; set; }

    /// <summary>
    /// Gets or sets the selected start page.
    /// </summary>
    /// <value>
    /// The selected start page.
    /// </value>
    public string SelectedStartPage
    {
      get
      {
        return _selectedStartPage;
      }
      set
      {
        _selectedStartPage = value;
        if (string.IsNullOrEmpty(_selectedStartPage) == false)
        { 
          LoadPage(_selectedStartPage);
        }
        NotifyPropertyChanged("SelectedStartPage");
      }

    }
    /// <summary>
    /// Gets or sets a value indicating whether this instance is dirty.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is dirty; otherwise, <c>false</c>.
    /// </value>
    public bool IsDirty
    {

      get => DirtyTracker.IsDirty;
      set => NotifyPropertyChanged("IsDirty");
    }
    /// <summary>
    /// Gets or sets a value indicating whether this instance is busy.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
    /// </value>
    public bool IsBusy
    {
      get => _isBusy;
      set
      {
        _isBusy = value;
        NotifyPropertyChanged("IsBusy");
      }
    }
    /// <summary>
    /// Gets or sets the project groups.
    /// </summary>
    /// <value>
    /// The project groups.
    /// </value>
    public ObservableCollection<ProjectGroupViewModel> ProjectGroups { get; set; }

    private readonly IDialogService _dialogService;
    private readonly IFileSystem _fileSystem;
    /// <summary>
    /// Initializes a new instance of the <see cref="StartPageViewModel" /> class.
    /// </summary>
    /// <param name="dialogService">The dialog service.</param>
    /// <param name="startPageGenerator">The start page generator.</param>
    /// <param name="fileSystem">The file system.</param>
    /// <param name="startPageRepository">The start page repository.</param>
    public StartPageViewModel(IDialogService dialogService, 
      IStartPageGenerator startPageGenerator,
      IFileSystem fileSystem,
      IJsonFileRepository<StartPageModel> startPageRepository)
    {
      _startPageRepository = startPageRepository;
      _dialogService = dialogService;
      _startPageGenerator = startPageGenerator;
      _fileSystem = fileSystem;
      this.StartPageFiles = new ObservableCollection<string>(StartPageEnvironment.GetStartPageFiles());
      this.ProjectGroups = new ObservableCollection<ProjectGroupViewModel>();
      this.AddProjectCommand = new RelayCommand(null,() => { this.AddProject(this.SelectedItem); });
      this.DeleteProjectCommand = new RelayCommand<ProjectViewModel>(null,this.DeleteProject);
      this.DeleteProjectGroupCommand = new RelayCommand<ProjectGroupViewModel>(null,this.DeleteProjectGroup);
      this.CreatePageCommand = new RelayCommand(() => this.IsBusy == false, this.CreatePage);
      this.SavePageCommand = new RelayCommand(() => this.IsBusy == false, this.SavePage);
      this.PreviewPageCommand = new RelayCommand(() => this.IsBusy == false, this.PreviewPage);
      this.AddGroupFromFolderCommand = new RelayCommand(() => this.IsBusy == false, () => { this.AddGroupFromFolder(); });
      this.AddEmptyGroupCommand = new RelayCommand(() => this.IsBusy == false, this.AddEmptyGroup);
      this.AddGroupButtonDropdownCommand = new RelayCommand<object>(null, this.AddGroupButtonDropdown);
      this.ConfigureFormCommand = new RelayCommand(this.ConfigureForm);
      this.MoveSelectedTreeItemCommand = new RelayCommand<object>(null,(o) => { this.MoveSelectedItem((Direction)o); });
      this.ProjectGroups.CollectionChanged += (o, e) => { DirtyTracker.IsDirty = true; };
      DirtyTracker.DirtyChanged += (o,e) => { this.IsDirty = e.Dirty; };
      this.SelectedStartPage = this.StartPageFiles.FirstOrDefault();
    }

    /// <summary>
    /// Creates the page.
    /// </summary>
    private void CreatePage()
    {
      StartPageGeneratorForm generatorForm = new StartPageGeneratorForm();
      generatorForm.ShowDialog();
      var data = generatorForm.DataContext as StartPageGeneratorViewModel;
      if (data != null)
      {
        if (data.GeneratedModel != null)
        {
          StartPageViewModelMapper.MapFrom(data.GeneratedModel, this);
          this.SelectedStartPage = null;
        }
        else if (data.CreateEmpty)
        {
          this.Clear();
        }
      }
    }
    /// <summary>
    /// Loads the page.
    /// </summary>
    /// <param name="page">The page.</param>
    public void LoadPage(string page)
    {
      string file = Path.Combine(StartPageEnvironment.StartPageFolder, page);
      if(_fileSystem.FileExists(file) == false)
      {
        return;
      }
      var startPageModel = _startPageRepository.Load(file);
      StartPageViewModelMapper.MapFrom(startPageModel,this);
      this.SelectedItem = null;
      DirtyTracker.IsDirty = false;
    }

    /// <summary>
    /// Saves the page.
    /// </summary>
    public void SavePage()
    {
      bool newPage = string.IsNullOrEmpty(this.SelectedStartPage);

      if (newPage)
      {
        string file = _dialogService.SaveFileDialog("json",StartPageEnvironment.StartPageFolder);
        if (string.IsNullOrEmpty(file))
        {
          return;
        }
        this.SelectedStartPage = Path.GetFileName(file);
      }

      _startPageRepository.SaveAs(StartPageViewModelMapper.MapTo(this), this.SelectedStartPage);
      DirtyTracker.IsDirty = false;
      if (newPage)
      {
        this.StartPageFiles.Add(this.SelectedStartPage);
      }
    }
    /// <summary>
    /// Clears this instance.
    /// </summary>
    public void Clear()
    {
      this.ProjectGroups.Clear();
      this.SelectedStartPage = null;
    }
    // TODO: this breaks MVVM pattern
    /// <summary>
    /// Adds the group button dropdown.
    /// </summary>
    /// <param name="sender">The sender.</param>
    private void AddGroupButtonDropdown(object sender)
    {
      if (this.IsBusy)
      {
        _startPageGenerator.Cancel();
        this.IsBusy = false;
      }
      else
      {
        if (sender is Button button)
        {
          if (button.ContextMenu != null)
          {
            button.ContextMenu.IsEnabled = true;
            button.ContextMenu.PlacementTarget = (sender as Button);
            button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            button.ContextMenu.IsOpen = true;
          }
        }
      }
    }
    /// <summary>
    /// Configures the form.
    /// </summary>
    private void ConfigureForm()
    {
      StartPageConfiguratorForm configureForm = new StartPageConfiguratorForm();
      configureForm.Show();
    }
    /// <summary>
    /// Adds the group from folder.
    /// </summary>
    /// <param name="folder">The folder.</param>
    public async void AddGroupFromFolder(string folder = "")
    {
      if (string.IsNullOrEmpty(folder))
      {
        folder = _dialogService.FolderDialog();
        if(string.IsNullOrEmpty(folder))
        {
          return;
        }
      }
      this.IsBusy = true;
      var startPage = await _startPageGenerator.GenerateAsync(new string[] { folder });

      if (_startPageGenerator.Cancelled == false)
      {
        if (startPage.ProjectGroups.Count == 0)
        {
          _dialogService.MessageBox("No projects found.");
        }
        else
        {
          int last = this.ProjectGroups.Any() ? this.ProjectGroups.Max(t => t.Index) + 1 : 0;
          foreach (var group in startPage.ProjectGroups)
          {
            var groupViewModel = ProjectGroupViewModelMapper.MapFrom(group);
            groupViewModel.Index = last++;
            this.ProjectGroups.Add(groupViewModel);
          }
        }
      }
      this.IsBusy = false;
    }
    /// <summary>
    /// Adds the empty group.
    /// </summary>
    private void AddEmptyGroup()
    {
      int newIndex = this.ProjectGroups.Any() ? this.ProjectGroups.Max(t => t.Index) + 1 : 0 + 1;
      this.ProjectGroups.Add(new ProjectGroupViewModel
      {
        Name = "New Group",
        Background = "WhiteSmoke",
        Foreground = "White",
        Expanded = true,
        Index = newIndex
      });
    }
    /// <summary>
    /// Moves the selected item.
    /// </summary>
    /// <param name="direction">The direction.</param>
    private void MoveSelectedItem(Direction direction)
    {
      if (this.SelectedItem != null)
      {
        
        if (this.SelectedItem is ProjectViewModel project)
        {
          if (project != null)
          {
            var group = this.MoveProject(project, direction);
            ProjectMoved?.Invoke(null, new ProjectMovedEventArgs(group, project));
          }
        }
        else if (this.SelectedItem is ProjectGroupViewModel projectGroup)
        {
          if (projectGroup != null)
          {
            this.MoveProjectGroup(projectGroup, direction);
          }
        }
      }

    }
    /// <summary>
    /// Deletes the project.
    /// </summary>
    /// <param name="project">The project.</param>
    public void DeleteProject(ProjectViewModel project)
    {
      var group = this.ProjectGroups.FirstOrDefault(p => p.Projects.Contains(project));
      group?.Projects.Remove(project);
    }
    /// <summary>
    /// Deletes the project group.
    /// </summary>
    /// <param name="group">The group.</param>
    public void DeleteProjectGroup(ProjectGroupViewModel group)
    {
      this.ProjectGroups.Remove(group);
    }

    /// <summary>
    /// Moves the project with a group selector
    /// </summary>
    /// <param name="project">The project.</param>
    /// <returns></returns>
    public ProjectGroupViewModel MoveProject(ProjectViewModel project)
    {
      var group = this.ProjectGroups.FirstOrDefault(p => p.Projects.Contains(project));
      if (group != null)
      {
        this.MoveProject(project, group);
        group.NotifyPropertyChanged("Projects");
      }
      return group;
    }
    /// <summary>
    /// Moves the project.
    /// </summary>
    /// <param name="project">The project.</param>
    /// <param name="direction">The direction.</param>
    /// <returns></returns>
    public ProjectGroupViewModel MoveProject(ProjectViewModel project, Direction direction)
    {
      var group = this.ProjectGroups.FirstOrDefault(p => p.Projects.Contains(project));
      if (group != null)
      {
        IEnumerable<ProjectViewModel> projects;
        if (direction == Direction.Up)
        {
          projects = group.Projects
            .OrderByDescending(f => f.Index);
        }
        else
        {
          projects = group.Projects
            .OrderBy(f => f.Index);
        }
        var swapProject = projects
          .SkipWhile(p => p.Index != project.Index)
          .Skip(1)
          .FirstOrDefault();

        if (swapProject != null)
        {
          int swapIndex = swapProject.Index;
          swapProject.Index = project.Index;
          project.Index = swapIndex;
        }
        ProjectMoved?.Invoke(null, new ProjectMovedEventArgs(group, project));

        group.NotifyPropertyChanged("Projects");
      }
      return group;
    }
    /// <summary>
    /// Moves the project.
    /// </summary>
    /// <param name="project">The project.</param>
    /// <param name="toGroup">To group.</param>
    public void MoveProject(ProjectViewModel project, ProjectGroupViewModel toGroup)
    {
      var group = this.ProjectGroups.First(p => p.Projects.Contains(project));
      group.Projects.Remove(project);

      var newGroup = this.ProjectGroups.First(p => p == toGroup);
      project.Index = newGroup.Projects.Any() ? newGroup.Projects.Max(t => t.Index) + 1 : 1;
      newGroup.Projects.Add(project);
      ProjectMoved?.Invoke(null, new ProjectMovedEventArgs(newGroup, project));

    }

    /// <summary>
    /// Moves the project group.
    /// </summary>
    /// <param name="projectGroup">The project group.</param>
    /// <param name="direction">The direction.</param>
    public void MoveProjectGroup(ProjectGroupViewModel projectGroup, Direction direction)
    {
      IEnumerable<ProjectGroupViewModel> groups;
      if (direction == Direction.Up)
      {
        groups = this.ProjectGroups
          .OrderByDescending(f => f.Index);
      }
      else
      {
        groups = this.ProjectGroups
          .OrderBy(f => f.Index);
      }
      var swapProjectGroup = groups
        .SkipWhile(p => p.Index != projectGroup.Index)
        .Skip(1)
        .FirstOrDefault();

      if (swapProjectGroup != null)
      {
        int swapIndex = swapProjectGroup.Index;
        swapProjectGroup.Index = projectGroup.Index;
        projectGroup.Index = swapIndex;
      }
    }

    /// <summary>
    /// Adds the project.
    /// </summary>
    public void AddProject(object toProjectGroup)
    {
      if (toProjectGroup is ProjectGroupViewModel targetGroup)
      {
        string fileName = _dialogService.AddProjectDialog();
        if (string.IsNullOrEmpty(fileName) == false)
        {
          int index = 1;
          if (targetGroup.Projects.Any())
          {
            index = targetGroup.Projects.Max(t => t.Index) + 1;
          }
          var project = new ProjectViewModel
          {
            Name = Path.GetFileName(fileName),
            Location = fileName,
            Index = index,
          };
          targetGroup.Projects.Add(project);
          ProjectMoved?.Invoke(null, new ProjectMovedEventArgs(targetGroup, project));
        }
      }
    }
    /// <summary>
    /// Previews the page.
    /// </summary>
    public void PreviewPage()
    {
      StartPagePreviewForm previewForm;
      if (string.IsNullOrEmpty(this.SelectedStartPage) == false && this.IsDirty == false)
      {
        previewForm = new StartPagePreviewForm(this.SelectedStartPage);
      }
      else
      {
        previewForm = new StartPagePreviewForm(StartPageViewModelMapper.MapTo(this));
      }
      previewForm.Show();
    }

    /// <summary>
    /// Notifies the property changed.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    private void NotifyPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

  }
}
