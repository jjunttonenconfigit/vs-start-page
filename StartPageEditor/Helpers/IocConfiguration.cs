using Ninject.Modules;
using StartPageControl.Helpers;
using StartPageControl.Models;
using StartPageEditor.Models;

namespace StartPageEditor.Helpers
{
  /// <summary>
  /// IocConfiguration
  /// </summary>
  /// <seealso cref="Ninject.Modules.NinjectModule" />
  internal class IocConfiguration : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      Bind<IDialogService>().To<DialogService>().InSingletonScope();
      Bind<IStartPageGenerator>().To<StartPageGenerator>().InTransientScope();
      Bind<IFileSystem>().To<FileSystem>().InSingletonScope();
      Bind<IFileFinder>().To<FileFinder>().InTransientScope();
      Bind<StartPageViewModel>().ToSelf().InSingletonScope();
      Bind<StartPageGeneratorViewModel>().ToSelf().InTransientScope();
      Bind<StartPageConfiguratorViewModel>().ToSelf().InSingletonScope();
      Bind<IJsonFileRepository<StartPageModel>>().To<StartPageRepository>().InTransientScope();
      Bind<IJsonFileRepository<AppSettings>>().To<AppSettingsRepository>().InSingletonScope();
    }
  }
}
