using StartPageEditor.Models;

namespace StartPageEditor.Helpers
{
  /// <summary>
  /// ViewModelLocator
  /// </summary>
  public class ViewModelLocator
  {
    /// <summary>
    /// Gets the start page view model.
    /// </summary>
    /// <value>
    /// The start page view model.
    /// </value>
    public StartPageViewModel StartPageViewModel => IocKernel.Get<StartPageViewModel>();

    /// <summary>
    /// Gets the start page generator view model.
    /// </summary>
    /// <value>
    /// The start page generator view model.
    /// </value>
    public StartPageGeneratorViewModel StartPageGeneratorViewModel => IocKernel.Get<StartPageGeneratorViewModel>();

    /// <summary>
    /// Gets the start page configurator view model.
    /// </summary>
    /// <value>
    /// The start page configurator view model.
    /// </value>
    public StartPageConfiguratorViewModel StartPageConfiguratorViewModel => IocKernel.Get<StartPageConfiguratorViewModel>();

    /// <summary>
    /// Gets the start page preview view model.
    /// </summary>
    /// <value>
    /// The start page preview view model.
    /// </value>
    public StartPagePreviewViewModel StartPagePreviewViewModel => IocKernel.Get<StartPagePreviewViewModel>();
  }
}
