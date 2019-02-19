using StartPageEditor.Models;
using StartPageControl.Models;
using System.Windows;

namespace StartPageEditor
{
  /// <summary>
  /// Interaction logic for StartPagePreview.xaml
  /// </summary>
  public partial class StartPagePreviewForm: Window
  {
    private readonly StartPagePreviewViewModel _startPagePreviewViewModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartPagePreviewForm"/> class.
    /// </summary>
    public StartPagePreviewForm()
    {
      InitializeComponent();

      _startPagePreviewViewModel = new StartPagePreviewViewModel((StartPageViewerViewModel)this.StartPagePreviewControl.DataContext);
      this.DataContext = _startPagePreviewViewModel;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="StartPagePreviewForm"/> class.
    /// </summary>
    /// <param name="startPageFile">The start page file.</param>
    public StartPagePreviewForm(string startPageFile) : this()
    {
      _startPagePreviewViewModel.SelectedFile = startPageFile;

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="StartPagePreviewForm"/> class.
    /// </summary>
    /// <param name="startPageModel">The start page model.</param>
    public StartPagePreviewForm(StartPageModel startPageModel) : this()
    {
      _startPagePreviewViewModel.SelectedModel = startPageModel;
    }

  }
}
