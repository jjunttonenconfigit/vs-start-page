using System.Linq;
using StartPageEditor.Helpers;
using System.Windows;

namespace StartPageEditor
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      IocKernel.Initialize(new IocConfiguration());
      base.OnStartup(e);
    }

    void App_Startup(object sender, StartupEventArgs e)
    {
      Window form;
      if (e.Args.Any() && e.Args.First() == "/preview")
      {
        form = new StartPagePreviewForm("default.json");
      }
      else
      {
        form = new StartPageEditorForm();
      }
      form.Show();

    }

  }
}
