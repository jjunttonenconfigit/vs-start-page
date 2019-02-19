using StartPageControl.Builders;
using StartPageControl.Commands;
using StartPageControl.Controls;
using StartPageControl.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace StartPageControl.Models
{
  /// <summary>
  /// StartPageViewerViewModel - view model for the start page viewer control
  /// </summary>
  /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
  public class StartPageViewerViewModel : INotifyPropertyChanged
  {
    private bool _loaded = true;
    private object _startPageContent;
    private bool _resizeRequested;
    private double _height;
    private readonly StartPageRepository _startPageRepository;
    private readonly StartPageCanvas _startPageCanvas;
    private List<RenderModel> _renderModels;
    private readonly RenderModelsBuilder _renderModelsBuilder;
    private double _scale = 1;
    private ObservableCollection<MenuItem> _menuItems;

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
    /// <summary>
    /// Gets or sets the menu items.
    /// </summary>
    /// <value>
    /// The menu items.
    /// </value>
    public ObservableCollection<MenuItem> MenuItems
    {
      get => _menuItems ?? (_menuItems = CreateMenu());
      set
      {
        _menuItems = value;
        NotifyPropertyChanged("MenuItems");
      }
    }

    /// <summary>
    /// Gets or sets the start content of the page.
    /// </summary>
    /// <value>
    /// The start content of the page.
    /// </value>
    public object StartPageContent
    {
      get => _startPageContent;
      set
      {
        _startPageContent = value;
        NotifyPropertyChanged("StartPageContent");
      }
    }
    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    /// <value>
    /// The height.
    /// </value>
    public double Height
    {
      get => _height;
      set
      {
        _height = value;
        HeightChanged();
      }
    }
    /// <summary>
    /// Gets or sets the scale up command.
    /// </summary>
    /// <value>
    /// The scale up command.
    /// </value>
    public ICommand ScaleUpCommand { get; set; }
    /// <summary>
    /// Gets or sets the scale down command.
    /// </summary>
    /// <value>
    /// The scale down command.
    /// </value>
    public ICommand ScaleDownCommand { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="StartPageViewerViewModel"/> class.
    /// </summary>
    public StartPageViewerViewModel()
    {
      this.ScaleUpCommand = new RelayCommand(null, this.ScaleUp);
      this.ScaleDownCommand = new RelayCommand(null, this.ScaleDown);

      _startPageRepository = new StartPageRepository();
      _renderModelsBuilder = new RenderModelsBuilder();
      _startPageCanvas = new StartPageCanvas
      {
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top,
      };
    }

    /// <summary>
    /// Loads from file.
    /// </summary>
    /// <param name="page">The page.</param>
    public void LoadFromFile(string page)
    {
      var startPageModel = _startPageRepository.Load(page);
      if (startPageModel != null)
      {
        _renderModels = _renderModelsBuilder.Build(startPageModel);
        this.Refresh();
      }
    }
    /// <summary>
    /// Loads from model.
    /// </summary>
    /// <param name="model">The model.</param>
    public void LoadFromModel(StartPageModel model)
    {
      _renderModels = _renderModelsBuilder.Build(model);
    }
    /// <summary>
    /// Refreshes this instance.
    /// </summary>
    public void Refresh()
    {
      if (_renderModels != null && this.Height > 0)
      {
        this.StartPageContent = _startPageCanvas.Render(_renderModels, Math.Max(this.Height, 120));
        _resizeRequested = false;
      }
    }

    /// <summary>
    /// Expands the project group control.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="expanded">if set to <c>true</c> [expanded].</param>
    internal void ExpandProjectGroupControl(ProjectGroupControl control, bool expanded)
    {
      _renderModels
        .First(t => t.Index == control.Index)
        .Expanded = expanded;

      this.Refresh();
    }

    /// <summary>
    /// Creates the menu.
    /// </summary>
    /// <returns></returns>
    private ObservableCollection<MenuItem> CreateMenu()
    {
      var menuItems = new ObservableCollection<MenuItem>();
      var layoutMenu = new MenuItem
      {
        Header = "Layout"
      };
      var save = new MenuItem { Header = "Save" };
      save.Click += delegate
      {
        if (_startPageRepository.Data != null && _startPageRepository.FileName != "")
        {
          _startPageRepository.Update(_renderModels);
        }
      };
      var expand = new MenuItem { Header = "Expand All" };
      expand.Click += delegate
      {
        foreach (var p in _renderModels)
        {
          p.Expanded = true;
        }
        this.Refresh();
      };
      var collapse = new MenuItem { Header = "Collapse All" };
      collapse.Click += delegate
      {
        foreach (var p in _renderModels)
        {
          p.Expanded = false;
        }
        this.Refresh();
      };
      var scaleUp = new MenuItem { Header = "Scale Up" };
      scaleUp.Click += delegate
      {
        this.ScaleUp();
      };
      var scaleDown = new MenuItem { Header = "Scale Down" };
      scaleDown.Click += delegate
      {
        this.ScaleDown();
      };
      
      layoutMenu.Items.Add(save);
      layoutMenu.Items.Add(expand);
      layoutMenu.Items.Add(collapse);
      layoutMenu.Items.Add(scaleUp);
      layoutMenu.Items.Add(scaleDown);
      menuItems.Add(layoutMenu);

      foreach (var file in StartPageEnvironment.GetStartPageFiles())
      {
        var menuItem = new MenuItem {Header = file};
        menuItem.Click += delegate
        {
          this.LoadFromFile(file);
        };
        menuItems.Add(menuItem);
      }
      return menuItems;
    }

    /// <summary>
    /// Scales up.
    /// </summary>
    public void ScaleUp()
    {
      _scale += .1;
      this.Scale(_scale);

    }
    /// <summary>
    /// Scales down.
    /// </summary>
    public void ScaleDown()
    {
      _scale -= .1;
      this.Scale(_scale);

    }
    /// <summary>
    /// Scales the specified percent.
    /// </summary>
    /// <param name="percent">The percent.</param>
    private void Scale(double percent)
    {
      TransformGroup g = new TransformGroup();
      g.Children.Add(new ScaleTransform(percent, percent));
      _startPageCanvas.LayoutTransform = g;
    }

    /// <summary>
    /// Notifies the property changed.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    private void NotifyPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    /// <summary>
    /// Heights the changed.
    /// </summary>
    private void HeightChanged()
    {
      if (_loaded && _resizeRequested == false)
      {
        _resizeRequested = true;
        Dispatcher.CurrentDispatcher.BeginInvoke(new Action(this.Refresh), DispatcherPriority.ContextIdle);
      }
    }
  }
}
